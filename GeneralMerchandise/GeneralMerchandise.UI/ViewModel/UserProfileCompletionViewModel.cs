using GeneralMerchandise.Data.Client;
using GeneralMerchandise.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.ViewModel
{
    class UserProfileCompletionViewModel : ViewModel
    {
        public static readonly int USER_ID_PARAMETER = 1;

        public UserModel User { get; private set; } = UserModel.NewInstance();

        private readonly UserOperation userOperation = new UserOperation();

        public UserProfileCompletionViewModel()
        {

        }

        protected override void OnParameterSet(IDictionary<int, object> parameters)
        {
            if(parameters.ContainsKey(USER_ID_PARAMETER))
            {
                User.Id = (int)parameters[USER_ID_PARAMETER];
            }
            base.OnParameterSet(parameters);
        }


    }
}
