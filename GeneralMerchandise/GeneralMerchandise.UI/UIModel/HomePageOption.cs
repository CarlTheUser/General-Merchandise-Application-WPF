using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.UIModel
{
    public class HomePageOption : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly string title;
        private readonly string description;
        private readonly string icon;

        private readonly Action selectionAction;

        public string Title => title;
        public string Description => description;
        public string Icon => icon;

        public Action SelectionAction => selectionAction;

        public HomePageOption(string title, string description, string icon) : this(title, description, icon, null) { }

        public HomePageOption(string title, string description, string icon, Action selectionAction)
        {
            this.title = title;
            this.description = description;
            this.icon = icon;
            this.selectionAction = selectionAction;
        }

        
    }
}
