using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.ViewLauncher
{
    public interface ViewLauncher
    {
        void Launch(IDictionary<int, object> parameters);
    }
}
