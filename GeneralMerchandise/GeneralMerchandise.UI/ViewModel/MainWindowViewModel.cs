using GeneralMerchandise.CommonTypes;
using GeneralMerchandise.UI.Navigation;
using GeneralMerchandise.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GeneralMerchandise.UI.ViewModel
{
    public class MainWindowViewModel : ViewModel, IMainView, IUserNavigation
    {
        
        public ICommand NavigateCommand { get; private set; }

        public ICommand NavigateBackCommand { get; private set; }

        public ICommand NavigateHomeCommand { get; private set; }

        public event EventHandler<PrenavigationEventArgs> Prenavigate;

        public IUserNavigation UserNavigation => this;

        private readonly Stack<NavigationItem> navigationStack = new Stack<NavigationItem>();

        private ApplicationPage ApplicationPage { get; set; }
        
        private Page currentPage = null;

        public Page CurrentPage
        {
            get => currentPage;
            private set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
                OnPropertyChanged("HasBackStack");
            }
        }
        
        public bool HasBackStack => navigationStack.Count > 0;

        public bool HomeNavigationVisible
        {
            get => true;
        }
        
        public MainWindowViewModel()
        {
            LoginHandle.Instance.AccountLoggedIn += Instance_AccountLoggedIn;
            LoginHandle.Instance.AccountLoggedOut += Instance_AccountLoggedOut;
            DisplayLogin();
        }

        #region Public Behaviors

        public bool Navigate(NavigationItem navigationItem)
        {
            ApplicationPage navigationTarget = navigationItem.NavigationPage;
            if (ApplicationPage != navigationTarget)
            {
                if(!CheckCancelNavigation(navigationTarget))
                {
                    BasePage page = CreatePage(navigationTarget);

                    if (navigationItem.AddToNavigationStack) navigationStack.Push(navigationItem);
                    if (navigationItem.HasParameters) page.GetViewModel().Parameters = navigationItem.Parameters;
                    ApplicationPage = navigationTarget;
                    CurrentPage = page;
                    OnPropertyChanged("HasBackStack");
                    return true;
                }

            }
            return false;
        }

        public void NavigateBack()
        {
            NavigationItem Current = navigationStack.Peek();


        }

        public void NavigateHome()
        {
            ClearNavigationStack();
            //
        }

        #endregion

        #region Private Methods

        private void ClearNavigationStack() => navigationStack.Clear();

        private bool CheckCancelNavigation(ApplicationPage page)
        {
            PrenavigationEventArgs eventArgs = new PrenavigationEventArgs(page);
            Prenavigate?.Invoke(this, eventArgs);
            return eventArgs.CancelNavigation;
        }

        private BasePage CreatePage(ApplicationPage applicationPage)
        {
            return (BasePage)Activator.CreateInstance(BasePage.ApplicationPageRegistry[applicationPage]);
        }

        private void DisplayLogin()
        {
            Navigate(new NavigationItem(ApplicationPage.Login, false));
        }

        #endregion

        #region Event Handlers
        private void Instance_AccountLoggedIn(object sender, LoginHandle.AccountLoggedInEventArgs e)
        {
            AccessType accessType = e.AccountModel.AccessType;
            switch(accessType)
            {
                //Show screens appropriate for access type
            }
        }

        private void Instance_AccountLoggedOut(object sender, EventArgs e)
        {
            ClearNavigationStack();
            DisplayLogin();
        }

        #endregion

        
    }
}
