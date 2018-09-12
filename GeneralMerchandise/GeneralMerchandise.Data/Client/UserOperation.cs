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
        AccountQuery accountQuery = new AccountQuery();

        UserQuery userQuery = new UserQuery();

        public UserOperation()
        {
            foo();
        }

        public void foo()
        {
            //accountQuery.Filter = new AccountQuery.UsernameFilter("John").And(new AccountQuery.AccessTypeFilter(Common.Type.AccessType.Administrator).Or(new AccountQuery.ActiveFilter(true)));
            //accountQuery.Ordering = AccountQuery.OrderByIdAsc().ChainWith(AccountQuery.OrderByActiveDesc());
            //accountQuery.Execute();

            //userQuery.Filter = new UserQuery.GenderFilter(Common.Type.Gender.Male);
            //userQuery.Execute();

            //UserModel user = UserModel.New("Image.jpeg", "Juan", "", "Dela Cruz", Common.Type.Gender.Male, DateTime.Now, "09154268798", "juan@yahoo.com", "Cavite");
            //user.Identity = 1;
            //new MySQLUserPersistence().Save(user);
        }
    }


}
