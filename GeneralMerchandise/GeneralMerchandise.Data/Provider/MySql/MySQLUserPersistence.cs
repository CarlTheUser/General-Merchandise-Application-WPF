using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class MySQLUserPersistence : UserPersistence
    {
        #region Constants

        private static readonly string TABLE_NAME = "UserInfo";

        private static readonly string ID_COLUMN = "Id";

        private static readonly string IMAGEFILENAME_COLUMN = "ImageFilename";

        private static readonly string FIRSTNAME_COLUMN = "Firstname";

        private static readonly string MIDDLENAME_COLUMN = "Middlename";

        private static readonly string LASTNAME_COLUMN = "Lastname";

        private static readonly string GENDER_COLUMN = "Gender";

        private static readonly string BIRTHDATE_COLUMN = "Birthdate";

        private static readonly string CONTACTNUMBER_COLUMN = "ContactNumber";

        private static readonly string EMAIL_COLUMN = "Email";

        private static readonly string ADDRESS_COLUMN = "Address";

        private static readonly string INSERT_STATEMENT = 
            $"INSERT INTO {TABLE_NAME} ({ID_COLUMN}, {FIRSTNAME_COLUMN}, {MIDDLENAME_COLUMN}, {LASTNAME_COLUMN}, {GENDER_COLUMN}, {BIRTHDATE_COLUMN}, {CONTACTNUMBER_COLUMN}, {EMAIL_COLUMN}, {ADDRESS_COLUMN}) " +
            $"VALUES (@{ID_COLUMN}, @{FIRSTNAME_COLUMN}, @{MIDDLENAME_COLUMN}, @{LASTNAME_COLUMN}, @{GENDER_COLUMN}, @{BIRTHDATE_COLUMN}, @{CONTACTNUMBER_COLUMN}, @{EMAIL_COLUMN}, @{ADDRESS_COLUMN}) ";

        private static readonly string UPDATE_STATEMENT = 
            $"UPDATE {TABLE_NAME} " +
            $"SET {FIRSTNAME_COLUMN} = @{FIRSTNAME_COLUMN}, {MIDDLENAME_COLUMN} = @{MIDDLENAME_COLUMN}, {LASTNAME_COLUMN} = @{LASTNAME_COLUMN}, {GENDER_COLUMN} = @{GENDER_COLUMN}, {BIRTHDATE_COLUMN} = @{BIRTHDATE_COLUMN}, {CONTACTNUMBER_COLUMN} = @{CONTACTNUMBER_COLUMN}, {EMAIL_COLUMN} = @{EMAIL_COLUMN}, {ADDRESS_COLUMN} = @{ADDRESS_COLUMN} " +
            $"WHERE {ID_COLUMN} = @{ID_COLUMN} ";

        private static readonly string DELETE_STATEMENT = 
            $"DELETE FROM {TABLE_NAME} " +
            $"WHERE {ID_COLUMN} = @{ID_COLUMN} ";

        #endregion

        private readonly ISQLProvider sqlFactory = new MySQLProvider(DBConfiguration.ConnectionString);

        public override void Save(UserModel model)
        {
            SQLCaller caller = new SQLCaller(sqlFactory);

            DbConnection connection = sqlFactory.CreateConnection();

            DbParameter[] inputParameters = sqlFactory.CreateInputParameters(new Dictionary<string, object>()
            {
                { ID_COLUMN, model.Identity },
                { FIRSTNAME_COLUMN, model.Firstname },
                { MIDDLENAME_COLUMN, model.Middlename },
                { LASTNAME_COLUMN, model.Lastname },
                { GENDER_COLUMN, model.Gender },
                { BIRTHDATE_COLUMN, model.BirthDate },
                { CONTACTNUMBER_COLUMN, model.ContactNumber },
                { EMAIL_COLUMN, model.Email },
                { ADDRESS_COLUMN, model.Address}
            });
            
            DbCommand command = sqlFactory.CreateCommand(INSERT_STATEMENT, inputParameters);

            caller.ExecuteNonQuery(command);

        }

        public override void Edit(UserModel model)
        {
            SQLCaller caller = new SQLCaller(sqlFactory);

            DbConnection connection = sqlFactory.CreateConnection();

            DbParameter[] inputParameters = sqlFactory.CreateInputParameters(new Dictionary<string, object>()
            {
                { FIRSTNAME_COLUMN, model.Firstname },
                { MIDDLENAME_COLUMN, model.Middlename },
                { LASTNAME_COLUMN, model.Lastname },
                { GENDER_COLUMN, model.Gender },
                { BIRTHDATE_COLUMN, model.BirthDate },
                { CONTACTNUMBER_COLUMN, model.ContactNumber },
                { EMAIL_COLUMN, model.Email },
                { ADDRESS_COLUMN, model.Address},
                { ID_COLUMN, model.Identity }
            });

            DbCommand command = sqlFactory.CreateCommand(UPDATE_STATEMENT, inputParameters);

            caller.ExecuteNonQuery(command);
        }

        public override void Delete(UserModel model)
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
