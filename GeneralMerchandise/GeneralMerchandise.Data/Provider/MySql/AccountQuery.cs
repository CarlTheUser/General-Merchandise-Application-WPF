using GeneralMerchandise.Data.Model;
using System.Collections.Generic;
using System.Data.Common;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class AccountQuery : SqlQuery<AccountModel>
    {

        private static readonly string BaseQuery = "SELECT Id, Username, HashedPassword, Salt, AccessType, IsActive "
            + "FROM Accounts ";

        

        public override IEnumerable<AccountModel> Execute()
        {
            string query = BaseQuery;



            return null;
        }

        public class UsernameFilter : SqlFilterCriterion
        {

            public string Username { get; set; }

            public UsernameFilter()
            {
                UsesParameter = true;
            }

            public UsernameFilter(string username) : this()
            {
                Username = username;
            }

            public override string Evaluate()
            {
                return "Username = @Username";
            }

            public override DbParameter[] GetParameters()
            {
                return new DbParameter[] { new MySQLProvider().CreateInputParameter("@Username", Username) };
            }
        }
    }
}
