using GeneralMerchandise.Common.Type;
using GeneralMerchandise.Data.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class AccountQuery : SqlQuery<AccountModel>
    {
        #region Static Constants

        private static readonly string BaseQuery = 
            "SELECT Id, Username, HashedPassword, Salt, AccessType, IsActive "
            + "FROM Accounts ";

        public static readonly SqlOrderCriterion IdAscOrder = new SqlOrderCriterion("Id");

        public static readonly SqlOrderCriterion IdDescOrder = new SqlOrderCriterion("Id", SqlQuery<AccountModel>.SqlOrderCriterion.OrderOptions.Descensding);

        public static readonly SqlOrderCriterion UsernameAscOrder = new SqlQuery<AccountModel>.SqlOrderCriterion("Username");

        public static readonly SqlOrderCriterion UsernameDescOrder = new SqlQuery<AccountModel>.SqlOrderCriterion("Username", SqlQuery<AccountModel>.SqlOrderCriterion.OrderOptions.Descensding);

        public static readonly SqlOrderCriterion ActiveAscOrder = new SqlQuery<AccountModel>.SqlOrderCriterion("IsActive");

        public static readonly SqlOrderCriterion ActiveDescOrder = new SqlQuery<AccountModel>.SqlOrderCriterion("IsActive", SqlQuery<AccountModel>.SqlOrderCriterion.OrderOptions.Descensding);

        #endregion

        private List<AccountModel> MapUser(IDataReader reader)
        {
            List<AccountModel> accounts = new List<AccountModel>();

            using (reader)
            {
                int idColumn = reader.GetOrdinal("Id");
                int usernameColumn = reader.GetOrdinal("Username");
                int hashedPasswordColumn = reader.GetOrdinal("HashedPassword");
                int saltColumn = reader.GetOrdinal("Salt");
                int accessTypeColumn = reader.GetOrdinal("AccessType");
                int activeColumn = reader.GetOrdinal("IsActive");

                while(reader.Read())
                {
                    accounts.Add(
                        AccountModel.FromDB(
                            reader.GetInt32(idColumn),
                            reader.GetString(usernameColumn),
                            reader.GetString(saltColumn),
                            reader.GetString(hashedPasswordColumn),
                            AccessType.Administrator, //HAHAHA
                            reader.GetByte(activeColumn) > 0));
                }
            }

                return accounts;
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

                while(criterion.HasNextCriteria)
                {
                    criterion = criterion.NextCriteria;

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

        public class ActiveFilter : SqlFilterCriterion
        {

            public bool IsActive { get; set; }

            public ActiveFilter(bool active) : this()
            {
                IsActive = active;
            }

            public ActiveFilter() { UsesParameter = true; }

            public override DbParameter[] GetParameters()
            {
                return new DbParameter[] { new MySQLProvider().CreateInputParameter("@IsActive", IsActive) };
            }

            protected override string GetSQLClause()
            {
                return "IsActive = @IsActive ";
            }
        }
    }
}
