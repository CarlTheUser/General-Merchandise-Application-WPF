using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralMerchandise.Data.Model;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class MySQLAccountPersistence : AccountPersistence
    {

        #region Constants

        private static readonly string TABLE_NAME = "Accounts";

        private static readonly string ID_COLUMN = "Id";

        private static readonly string USERNAME_COLUMN = "Username";

        private static readonly string HASHEDPASSWORD_COLUMN = "HashedPassword";

        private static readonly string SALT_COLUMN = "Salt";

        private static readonly string ACCESSTYPE_COLUMN = "AccessType";

        private static readonly string ACTIVE_COLUMN = "IsActive";

        private static readonly string INSERT_STATEMENT = $"INSERT INTO {TABLE_NAME}({USERNAME_COLUMN}, {HASHEDPASSWORD_COLUMN}, {SALT_COLUMN}, {ACCESSTYPE_COLUMN}, {ACTIVE_COLUMN}) " +
            $"VALUES(@{USERNAME_COLUMN}, @{HASHEDPASSWORD_COLUMN}, @{SALT_COLUMN}, @{ACCESSTYPE_COLUMN}, @{ACTIVE_COLUMN})";

        private static readonly string UPDATE_STATEMENT = $"UPDATE {TABLE_NAME} " +
            $"SET {USERNAME_COLUMN} = @{USERNAME_COLUMN}, {ACCESSTYPE_COLUMN} = @{ACCESSTYPE_COLUMN}, {ACTIVE_COLUMN} = @{ACTIVE_COLUMN} " +
            $"WHERE {ID_COLUMN} = @{ID_COLUMN} ";

        private static readonly string DELETE_STATEMENT = $"DELETE FROM {TABLE_NAME} " +
            $"WHERE {ID_COLUMN} = @{ID_COLUMN}";

        #endregion

        private readonly ISQLProvider sqlFactory = new MySQLProvider(DBConfiguration.ConnectionString);
        
        public override void Save(AccountModel model)
        {
            SQLCaller caller = new SQLCaller(sqlFactory);

            DbConnection connection = sqlFactory.CreateConnection();

            DbParameter[] inputParameters = sqlFactory.CreateInputParameters(new Dictionary<string, object>()
            {
                { USERNAME_COLUMN, model.Username },
                { HASHEDPASSWORD_COLUMN, model.Password.HashedPassword },
                { SALT_COLUMN, model.Password.Salt },
                { ACCESSTYPE_COLUMN, model.AccessType },
                { ACTIVE_COLUMN, model.IsActive }
            });

            DbParameter idParameter = sqlFactory.CreateOutputParameter(ID_COLUMN);

            DbCommand command = sqlFactory.CreateCommand(INSERT_STATEMENT, inputParameters, new DbParameter[] { idParameter });

            caller.ExecuteNonQuery(command);

            model.Identity = Convert.ToInt32(idParameter.Value);
        }

        public override void Edit(AccountModel model)
        {
            SQLCaller caller = new SQLCaller(sqlFactory);

            DbConnection connection = sqlFactory.CreateConnection();

            DbParameter[] inputParameters = sqlFactory.CreateInputParameters(new Dictionary<string, object>()
            {
                { USERNAME_COLUMN, model.Username },
                { ACCESSTYPE_COLUMN, model.AccessType },
                { ACTIVE_COLUMN, model.IsActive },
                { ID_COLUMN, model.Identity }
            });

            DbCommand command = sqlFactory.CreateCommand(UPDATE_STATEMENT, inputParameters);

            caller.ExecuteNonQuery(command);
        }

        public override void Delete(AccountModel model)
        {
            SQLCaller caller = new SQLCaller(sqlFactory);

            DbConnection connection = sqlFactory.CreateConnection();

            DbParameter[] inputParameters = sqlFactory.CreateInputParameters(new Dictionary<string, object>()
            {
                { ID_COLUMN, model.Identity }
            });

            DbCommand command = sqlFactory.CreateCommand(DELETE_STATEMENT, inputParameters);

            caller.ExecuteNonQuery(command);
        }
        
    }
}
