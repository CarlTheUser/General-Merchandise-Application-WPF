using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeneralMerchandise.UI
{
    sealed class NotificationHub
    {
        public INotifiable CurrentNotificationHandler { get; set; }

        private static NotificationHub instance;

        private static readonly object thislock = new object();

        public static NotificationHub GetInstance()
        {
            // double checked locking (
            if (instance == null)
            {
                //threadsafe
                lock (thislock)
                {
                    instance = (instance == null) ? new NotificationHub() : instance;
                }
            }
            return instance;
        }

        private NotificationHub()
        {
            CurrentNotificationHandler = null;
        }


        public void ShowMessage(string message)
        {
            try { CurrentNotificationHandler.ShowMessage(message); }
            catch { MessageBox.Show(message); } // if current handler is null or error
        }

        public void Prompt(string message)
        {
            try { CurrentNotificationHandler.Prompt(message); }
            catch { MessageBox.Show(message); }
        }
    }
}
