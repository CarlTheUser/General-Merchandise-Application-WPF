using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.ViewLauncher
{
    class ProfileSetupWindowLauncher : IProfileSetupViewLauncher
    {
        private ProfileSetupWindow SetupWindow { get; set; }

        public void Launch(IDictionary<int, object> parameters)
        {
            SetupWindow = SetupWindow ?? new ProfileSetupWindow();
            SetupWindow.GetViewModel().Parameters = parameters;
            SetupWindow.Topmost = true;
            SetupWindow.Show();
        }

        public void Close()
        {
            SetupWindow.Close();
            SetupWindow = null;
        }
    }
}
