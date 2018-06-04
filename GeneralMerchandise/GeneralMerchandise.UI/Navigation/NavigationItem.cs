using GeneralMerchandise.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.Navigation
{
    public class NavigationItem
    {
        
        private readonly ApplicationPage navigationPage;

        public ApplicationPage NavigationPage => navigationPage;

        private readonly bool addToNavigationStack;

        public bool AddToNavigationStack => addToNavigationStack;

        private readonly IDictionary<int, object> parameters;

        public IDictionary<int, object> Parameters => parameters;

        public bool HasParameters => parameters != null;

        public NavigationItem(ApplicationPage page, bool addToNavigationStack = true, IDictionary<int, object> parameters = null)
        {
            this.navigationPage = page;
            this.addToNavigationStack = addToNavigationStack;
            this.parameters = parameters;
        }
    }
}