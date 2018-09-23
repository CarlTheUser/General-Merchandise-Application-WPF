using GeneralMerchandise.Common.Type;
using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class AccountQuery : SqlQuery<AccountModel>
    {
        #region Constants

        private static readonly string TABLE_NAME = "Accounts";

        private static readonly string ID_COLUMN = "Id";

        private static readonly string USERNAME_COLUMN = "Username";

        private static readonly string HASHEDPASSWORD_COLUMN = "HashedPassword";

        private static readonly string SALT_COLUMN = "Salt";

        private static readonly string ACCESSTYPE_COLUMN = "AccessType";

        private static readonly string ACTIVE_COLUMN = "IsActive";

        private static readonly string BASE_QUERY =
            $"SELECT {ID_COLUMN}, {USERNAME_COLUMN}, {HASHEDPASSWORD_COLUMN}, {SALT_COLUMN}, {ACCESSTYPE_COLUMN}, {ACTIVE_COLUMN} " +
            $"FROM {TABLE_NAME} ";

        #endregion

        #region Order Factory Methods

        public static SqlOrderCriterion OrderByIdAsc() { return new SqlOrderCriterion(ID_COLUMN); }

        public static SqlOrderCriterion OrderByIdDesc() { return new SqlOrderCriterion(ID_COLUMN, SqlQuery<AccountModel>.SqlOrderCriterion.OrderOptions.Descensding); }

        public static SqlOrderCriterion OrderByUsernameAsc() { return new SqlQuery<AccountModel>.SqlOrderCriterion(USERNAME_COLUMN); }

        public static SqlOrderCriterion OrderByUsernameDesc() { return new SqlQuery<AccountModel>.SqlOrderCriterion(USERNAME_COLUMN, SqlQuery<AccountModel>.SqlOrderCriterion.OrderOptions.Descensding); }

        public static SqlOrderCriterion OrderByActiveAsc() { return new SqlQuery<AccountModel>.SqlOrderCriterion(ACTIVE_COLUMN); }

        public static SqlOrderCriterion OrderByActiveDesc() { return new SqlQuery<AccountModel>.SqlOrderCriterion(ACTIVE_COLUMN, SqlQuery<AccountModel>.SqlOrderCriterion.OrderOptions.Descensding); }

        #endregion
        
        private List<AccountModel> MapAccounts(IDataReader reader)
        {
            List<AccountModel> accounts = new List<AccountModel>();

            using (reader)
            {
                int idColumn = reader.GetOrdinal(ID_COLUMN);
                int usernameColumn = reader.GetOrdinal(USERNAME_COLUMN);
                int hashedPasswordColumn = reader.GetOrdinal(HASHEDPASSWORD_COLUMN);
                int saltColumn = reader.GetOrdinal(SALT_COLUMN);
                int accessTypeColumn = reader.GetOrdinal(ACCESSTYPE_COLUMN);
                int activeColumn = reader.GetOrdinal(ACTIVE_COLUMN);

                while(reader.Read())
                {
                    accounts.Add(
                        AccountModel.Existing(
                            reader.GetInt32(idColumn),
                            reader.GetString(usernameColumn),
                            reader.GetString(saltColumn),
                            reader.GetString(hashedPasswordColumn),
                            GetAccessType(reader.GetByte(accessTypeColumn)),
                            reader.GetByte(activeColumn) > 0));
                }
            }

                return accounts;
        }
        
        private AccessType GetAccessType(byte accessType)
        {
            switch(accessType)
            {
                case 0: return AccessType.Cashier;
                case 1: return AccessType.Administrator;
                default: return AccessType.Cashier;
            }
        }

        public override IEnumerable<AccountModel> Execute()
        {
            IEnumerable<AccountModel> accounts = null;

            string query = BASE_QUERY;

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
           
            accounts = caller.Get(MapAccounts, command);

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
                return new DbParameter[] { new MySQLProvider().CreateInputParameter("@" + USERNAME_COLUMN, Username) };
            }

            protected override string GetSQLClause()
            {
                return USERNAME_COLUMN + " = @" + USERNAME_COLUMN + " ";
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
                return new DbParameter[] { new MySQLProvider().CreateInputParameter("@" + ACCESSTYPE_COLUMN, AccessType) };
            }

            protected override string GetSQLClause()
            {
                return ACCESSTYPE_COLUMN + " = @" + ACCESSTYPE_COLUMN + " ";
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
                return new DbParameter[] { new MySQLProvider().CreateInputParameter("@" + ACTIVE_COLUMN, IsActive) };
            }

            protected override string GetSQLClause()
            {
                return ACTIVE_COLUMN + " = @" + ACTIVE_COLUMN + " ";
            }
        }
    }
}
