using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool parameterSet = false;

        private IDictionary<int, object> parameters;

        public IDictionary<int, object> Parameters
        {
            get => parameters;
            set
            {
                if (!parameterSet)
                {
                    parameters = value;
                    parameterSet = true;
                    OnParameterSet(parameters);
                }
                else throw new Exception("Parameter for ViewModel can only be set once");
            }
        }

        public ViewModel() { }

        protected virtual void OnParameterSet(IDictionary<int, object> parameters) { }


    }
}
