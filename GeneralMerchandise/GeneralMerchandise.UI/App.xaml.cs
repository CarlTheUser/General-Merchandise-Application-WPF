using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GeneralMerchandise.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static readonly string APPLICATION_NAME = "General Merchandise Client";

        private Mutex mutex;

        private bool isNewInstance;

        public new static App Current { get; private set; }

        public IMainView MainView { get; private set; }

        public App() : base()
        {
            mutex = new Mutex(true, APPLICATION_NAME, out isNewInstance);

            if (!isNewInstance)
            {
                MessageBox.Show("An instance of the application is already running.", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                Application.Current.Shutdown();
            }

            Current = this;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            MainView = (IMainView)window.DataContext;
            MainView.UserNavigation.Navigate(new Navigation.NavigationItem(Pages.ApplicationPage.Login));
            window.Show();
        }

    }
}
