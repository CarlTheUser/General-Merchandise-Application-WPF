using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.Data.Model;
using GeneralMerchandise.Data.Provider.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Client
{
    public sealed class UserOperation
    {
        public UserOperation() { } 

        public void Edit(UserData user)
        {

        }

        public IEnumerable<UserData> GetUsers()
        {
            UserQuery query = new UserQuery();

            List<UserModel> userModels = query.Execute().ToList();

            return from user in userModels
                   select new UserData()
                   {
                       Id = user.Identity,
                       ImageFileLocation = $"{Configuration.Instance.UserImageDirectoryPath}\\{user.ImageFilename}",
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

            UserModel user = query.Execute().ToList()[0];

            if (user == null) return null;

            return new UserData()
            {
                Id = user.Identity,
                ImageFileLocation = $"{Configuration.Instance.UserImageDirectoryPath}\\{user.ImageFilename}",
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


    }


}
