using GeneralMerchandise.Common.Type;
using GeneralMerchandise.Data.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class AccountQuery : SqlQuery<AccountModel>
    {

        private static readonly string BaseQuery = 
            "SELECT Id, Username, HashedPassword, Salt, AccessType, IsActive "
            + "FROM Accounts ";

        private List<AccountModel> MapUser(IDataReader reader)
        {
            return null;
        }
        

        public override IEnumerable<AccountModel> Execute()
        {
            IEnumerable<AccountModel> accounts = null;

            string query = BaseQuery;

            SqlFilterCriterion criterion = (SqlFilterCriterion)Filter;

            List<DbParameter> parameters = new List<DbParameter>();

            bool hasParameter = false;

            if(criterion != null)
            {
                query += string.Format("WHERE {0} ", criterion.Evaluate());

                if (criterion.UsesParameter)
                {
                    parameters.AddRange(criterion.GetParameters());
                    hasParameter = true;
                }

                while(criterion.HasChainedCriteria)
                {
                    criterion = criterion.ChainedCriteria;

                    query += criterion.Evaluate() + " ";

                    if (criterion.UsesParameter)
                    {
                        parameters.AddRange(criterion.GetParameters());
                        hasParameter = true;
                    }
                }
            }

            if(Grouping != null) query += string.Format("GROUP BY {0} ", Grouping.Evaluate());

            if (Ordering != null) query += string.Format("ORDER BY {0} ", Ordering.Evaluate());

            MySQLProvider provider = new MySQLProvider(DBConfiguration.ConnectionString);
            SQLCaller caller = new SQLCaller(provider);

            DbCommand command = hasParameter ? provider.CreateCommand(query, parameters.ToArray()) : provider.CreateCommand(query);
           
            accounts = caller.Get(MapUser, command);

            return accounts;
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
            
            public override DbParameter[] GetParameters()
            {
                return new DbParameter[] { new MySQLProvider().CreateInputParameter("@Username", Username) };
            }

            protected override string GetSQLClause()
            {
                return "Username = @Username";
            }
        }

        public class AccessTypeFilter : SqlFilterCriterion
        {

            public AccessType AccessType { get; set; }

            public AccessTypeFilter() { UsesParameter = true; }

            public AccessTypeFilter(AccessType accessType) : this()
            {
                AccessType = accessType;
            }

            public override DbParameter[] GetParameters()
            {
                return new DbParameter[] { new MySQLProvider().CreateInputParameter("@AccessType", AccessType) };
            }

            protected override string GetSQLClause()
            {
                return "AccessType = @AccessType ";
            }
        }
    }
}
