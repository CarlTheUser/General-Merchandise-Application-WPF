using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.Data.Model;
using GeneralMerchandise.Data.Provider.MySql;
using GeneralMerchandise.Data.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Client
{
    public sealed class UserOperation : IFallibleOperation
    {
        public UserOperation() { }

        public event EventHandler<FallibleOperationEventArgs> ErrorOccured;

        public void Save(UserData user)
        {
            if (user == null) throw new ArgumentNullException("user");
            UserModel userModel = UserModel.Existing(
                user.Id,
                user.ImageFileLocation,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Gender,
                user.BirthDate,
                user.ContactNumber,
                user.Email,
                user.Address);

            UserValidator validator = new UserValidator(userModel);

            validator.Validate();

            if (validator.IsValid)
            {
                MySQLUserPersistence userPersistence = new MySQLUserPersistence();
                userPersistence.Save(userModel);
            }
            else OnErrorOccured(validator.BrokenRules[0]);
            
        }

        public void Edit(UserData user)
        {
            if (user == null) throw new ArgumentNullException("user");
            UserModel userModel = UserModel.Existing(
                user.Id,
                user.ImageFileLocation,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Gender,
                user.BirthDate,
                user.ContactNumber,
                user.Email,
                user.Address);

            UserValidator validator = new UserValidator(userModel);

            validator.Validate();

            if (validator.IsValid)
            {
                MySQLUserPersistence userPersistence = new MySQLUserPersistence();
                userPersistence.Edit(userModel);
            }
            else OnErrorOccured(validator.BrokenRules[0]);
        }

        public IEnumerable<UserData> GetUsers()
        {
            UserQuery query = new UserQuery();

            List<UserModel> userModels = query.Execute().ToList();

            return from user in userModels
                   select new UserData()
                   {
                       Id = user.Identity,
                       ImageFileLocation = $@"{Configuration.Instance.UserImageDirectoryPath}\{user.ImageFilename}",
                       Firstname = user.Firstname,
                       Middlename = user.Middlename,
                       Lastname = user.Lastname,
                       Gender = user.Gender,
                       BirthDate = user.BirthDate,
                       ContactNumber = user.ContactNumber,
                       Email = user.Email,
                       Address = user.Address
                   };
        }

        public UserData GetFromAccount(AccountData account)
        {
            if(account == null)
            {
                throw new ArgumentNullException("account");
            }
            UserQuery query = new UserQuery();
            query.Filter = new UserQuery.IdFilter(account.Id);

            var result = query.Execute().ToList();

            if (result.Count > 0)
            {
                UserModel user = result[0];
                return new UserData()
                {
                    Id = user.Identity,
                    ImageFileLocation = $@"{Configuration.Instance.UserImageDirectoryPath}\{user.ImageFilename}",
                    Firstname = user.Firstname,
                    Middlename = user.Middlename,
                    Lastname = user.Lastname,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    ContactNumber = user.ContactNumber,
                    Email = user.Email,
                    Address = user.Address
                };
            }

            else return null;            
        }

        void OnErrorOccured(string message)
        {
            ErrorOccured?.Invoke(this, new FallibleOperationEventArgs(message));
        }

    }


}
