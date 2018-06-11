using GeneralMerchandise.UI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI
{
    internal class LoginHandle : INotifyPropertyChanged
    {

        #region Singleton

        //CLR-Managed singleton
        private static readonly LoginHandle instance = new LoginHandle();

        public static LoginHandle Instance => instance;

        #endregion

        #region Events

        /// <summary>
        /// Event that fires when a user logged in so that application elements can hook up
        /// to changes.
        /// </summary>
        public event EventHandler<AccountLoggedInEventArgs> AccountLoggedIn;

        /// <summary>
        /// Event that fires when a user logged out so that application elements can hook up
        /// to changes.
        /// </summary>
        public event EventHandler AccountLoggedOut;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public AccountModel CurrentAccount { get; private set; }

        public bool HasLogin
        {
            get => CurrentAccount != null;
        }

        private LoginHandle() { }

        public void LoginAccount(AccountModel account)
        {
            OnLogin(CurrentAccount = account);
        }

        public void LogOut()
        {
            CurrentAccount = null;
            OnLogOut();
        }

        private void OnLogin(AccountModel account)
        {
            if (AccountLoggedIn != null) AccountLoggedIn.Invoke(this, new AccountLoggedInEventArgs(account));
            else
            {
                //Log no listener state
            }
            OnNotifyPropertyChanged("HasLogin");
            OnNotifyPropertyChanged("CurrentAccount");
        }

        private void OnLogOut()
        {
            if (AccountLoggedOut != null) AccountLoggedOut.Invoke(this, EventArgs.Empty);
            else
            {
                //Log no listener
            }
            OnNotifyPropertyChanged("HasLogin");
            OnNotifyPropertyChanged("CurrentAccount");
        }
        
        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal class AccountLoggedInEventArgs : EventArgs
        {
            private readonly AccountModel accountModel;

            public AccountModel AccountModel => accountModel;

            public AccountLoggedInEventArgs(AccountModel account)
            {
                accountModel = account;
            }
        }
    }
}
