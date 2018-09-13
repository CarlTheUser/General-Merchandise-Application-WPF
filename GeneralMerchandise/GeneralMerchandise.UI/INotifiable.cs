using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI
{
    interface INotifiable
    {
        void ShowMessage(string message);

        void Prompt(string message);
    }
}
