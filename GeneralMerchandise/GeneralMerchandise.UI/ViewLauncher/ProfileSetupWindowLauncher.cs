using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.ViewLauncher
{
    class ProfileSetupWindowLauncher : IProfileSetupViewLauncher
    {
        public void Launch(IDictionary<int, object> parameters)
        {
            ProfileSetupWindow window = new ProfileSetupWindow();
            window.GetViewModel().Parameters = parameters;
            window.Topmost = true;
            window.Show();
        }
    }
}
