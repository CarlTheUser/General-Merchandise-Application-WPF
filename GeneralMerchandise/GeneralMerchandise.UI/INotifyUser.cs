using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI
{
    interface INotifyUser
    {
        void ShowMessage(string message);

        void Prompt(string message);
    }
}
