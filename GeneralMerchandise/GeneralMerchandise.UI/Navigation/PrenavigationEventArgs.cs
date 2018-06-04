using GeneralMerchandise.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.Navigation
{
    public class PrenavigationEventArgs : EventArgs
    {
        private readonly ApplicationPage applicationPage;

        public ApplicationPage ApplicationPage => ApplicationPage;

        private bool cancelNavigation = false;

        public bool CancelNavigation
        {
            get => cancelNavigation;
            set
            {
                if (cancelNavigation) return;   //once the value is set to true it ignores following changes.
                cancelNavigation = value;
            }
        }

        public PrenavigationEventArgs(ApplicationPage page)
        {
            applicationPage = page;
        }
    }
}
