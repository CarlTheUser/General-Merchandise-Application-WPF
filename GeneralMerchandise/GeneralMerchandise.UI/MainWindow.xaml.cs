using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using GeneralMerchandise.UI.ViewModel;

namespace GeneralMerchandise.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IApplicationView, INotifyUser
    {
        private readonly Queue<string> messageQueue = new Queue<string>();

        private readonly DispatcherTimer popUpTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1), IsEnabled = false };

        private int popUpCounter = 3;

        private bool isMessageDisplaying = false;

        public MainWindow()
        {
            InitializeComponent();
            popUpTimer.Tick += PopUpTimer_Tick;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.EnableBlur();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }

        public ViewModel.ViewModel GetViewModel()
        {
            return VM;
        }


        

        

        private void PopUpTimer_Tick(object sender, EventArgs e)
        {
            if (--popUpCounter < 0)
            {
                skipCurrentCaptionMessage();
            }
        }

        private void skipCurrentCaptionMessage()
        {
            popUpTimer.Stop();
            toggleNotification(false);
            isMessageDisplaying = false;
            popUpCounter = 5;
        }

        private void updateMessageQueue()
        {
            if (messageQueue.Count > 0) ShowMessage(messageQueue.Dequeue());
        }
        public void ShowMessage(string message)
        {
            if (isMessageDisplaying)
            {
                messageQueue.Enqueue(message);
                popUpCounter -= 2;
            }
            else
            {
                notificationHandleText.Text = message;

                toggleNotification(true);

                isMessageDisplaying = true;

                popUpTimer.Start();
            }

        }


        private void toggleNotification(bool show)
        {
            Storyboard sb = Resources[show ? "sbShowNotification" : "sbHideNotification"] as Storyboard;
            if (show) notificationHandle.Visibility = Visibility.Visible;
            sb.Begin(notificationHandle);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    notificationHandle.Visibility = (notificationHandle.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;

        //}

        public void Prompt(string message)
        {
            throw new NotImplementedException();
        }

        private void HideNotification_Completed(object sender, EventArgs e)
        {
            notificationHandle.Visibility = Visibility.Collapsed;
            updateMessageQueue();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            NotificationHub.GetInstance().CurrentNotificationHandler = this;
        }

        private void notificationCloseButton_Click(object sender, RoutedEventArgs e)
        {
            skipCurrentCaptionMessage();
        }
    }
}
