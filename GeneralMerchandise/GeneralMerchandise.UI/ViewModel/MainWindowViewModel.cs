using GeneralMerchandise.UI.Navigation;
using GeneralMerchandise.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GeneralMerchandise.UI.ViewModel
{
    public class MainWindowViewModel : ViewModel, IMainView, IUserNavigation
    {
        
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


        public MainWindowViewModel()
        {

        }

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
            
        }

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
    }
}
