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

        public bool CancelNavigation { get; set; } = false;

        public PrenavigationEventArgs(ApplicationPage page)
        {
            applicationPage = page;
        }
    }
}
