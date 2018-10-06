using GeneralMerchandise.Common.Type;
using GeneralMerchandise.Data.Client;
using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneralMerchandise.UI.ViewModel
{
    class UserProfileCompletionViewModel : ViewModel
    {
        public static readonly int USER_ID_PARAMETER = 1;

        public UserModel User { get; private set; } = UserModel.NewInstance();

        public ICommand CompleteProfileCommand { get; private set; }

        public IEnumerable<Gender> GenderOptions { get; private set; }


        public UserProfileCompletionViewModel()
        {
            GenderOptions = Enum.GetValues(typeof(Gender)).Cast<Gender>();
        }

        protected override void OnParameterSet(IDictionary<int, object> parameters)
        {
            if(parameters.ContainsKey(USER_ID_PARAMETER))
            {
                User.Id = (int)parameters[USER_ID_PARAMETER];
            }
            base.OnParameterSet(parameters);
        }

        private void CompleteUserProfile()
        {
            UserOperation userOperation = new UserOperation();
            UserData userData = new UserData
            {
                Id = User.Id,
                ImageFileLocation = User.ImageFileLocation,
                Firstname = User.Firstname,
                Middlename = User.Middlename,
                Lastname = User.Lastname,
                Gender = User.Gender,
                BirthDate = User.BirthDate,
                ContactNumber = User.ContactNumber,
                Email = User.Email,
                Address = User.Address
            };
            userOperation.Save(userData);

        }


    }
}
