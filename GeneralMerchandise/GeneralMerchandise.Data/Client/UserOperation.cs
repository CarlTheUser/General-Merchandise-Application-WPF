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

        public UserOperation()
        {
            foo();
        }

        public void foo()
        {
            accountQuery.Filter = new AccountQuery.UsernameFilter("John").And(new AccountQuery.AccessTypeFilter(Common.Type.AccessType.Administrator).Or(new AccountQuery.ActiveFilter(true)));
            accountQuery.Ordering = AccountQuery.OrderByIdAsc().ChainWith(AccountQuery.OrderByActiveDesc());
            accountQuery.Execute();
        }
    }


}
