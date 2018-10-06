using GeneralMerchandise.Data.Client.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class UserDisplayQuery : SqlQuery<UserDisplayData>
    {
        //public int Id { get; set; }
        //public string Firstname { get; set; }
        //public string Middlename { get; set; }
        //public string Lastname { get; set; }
        //public bool IsActive { get; set; }
        //public DateTime Created { get; set; }

        #region Constants

        private static readonly string ACCOUNT_TABLE_NAME = "Accounts";

        private static readonly string USER_TABLE_NAME = "UserInfo";

        private static readonly string ID_COLUMN = "Id";

        private static readonly string IMAGEFILENAME_COLUMN = "ImageFilename";

        private static readonly string FIRSTNAME_COLUMN = "Firstname";

        private static readonly string MIDDLENAME_COLUMN = "Middlename";

        private static readonly string LASTNAME_COLUMN = "Lastname";

        private static readonly string ACTIVE_COLUMN = "IsActive";

        private static readonly string CREATED_COLUMN = "Created";

        private static readonly string BASE_QUERY = 
            $"SELECT a.{ID_COLUMN}, u.{IMAGEFILENAME_COLUMN}, u.{FIRSTNAME_COLUMN}, u.{MIDDLENAME_COLUMN}, u.{LASTNAME_COLUMN}, a.{ACTIVE_COLUMN}, a.{CREATED_COLUMN} " +
            $"FROM {ACCOUNT_TABLE_NAME} a " +
            $"JOIN {USER_TABLE_NAME} u " +
            $"ON {ACCOUNT_TABLE_NAME}.{ID_COLUMN} = {USER_TABLE_NAME}.{ID_COLUMN} ";

        #endregion

        private List<UserDisplayData> MapUserDisplay(IDataReader reader)
        {
            List<UserDisplayData> userDisplays = new List<UserDisplayData>();

            using (reader)
            {
                int idColumn = reader.GetOrdinal(ID_COLUMN);
                int imagefilenameColumn = reader.GetOrdinal(IMAGEFILENAME_COLUMN);
                int firstnameColumn = reader.GetOrdinal(FIRSTNAME_COLUMN);
                int middlenameColumn = reader.GetOrdinal(MIDDLENAME_COLUMN);
                int lastnameColumn = reader.GetOrdinal(LASTNAME_COLUMN);
                int activeColumn = reader.GetOrdinal(ACTIVE_COLUMN);
                int createdColumn = reader.GetOrdinal(CREATED_COLUMN);

                while (reader.Read())
                {
                    userDisplays.Add(new UserDisplayData()
                    {
                        Id = reader.GetInt32(idColumn),
                        ImageFileLocation = GetImagePath(reader.GetString(imagefilenameColumn)),
                        Firstname = reader.GetString(firstnameColumn),
                        Middlename = reader.GetString(middlenameColumn),
                        Lastname = reader.GetString(lastnameColumn),
                        IsActive = reader.GetBoolean(activeColumn),
                        Created = reader.GetDateTime(createdColumn)
                    });
                }
            }

            return userDisplays;
        }

        private string GetImagePath(string imageFile)
        {
            if (imageFile != null && imageFile.Trim().Length > 0)
            {
                return $"{Configuration.Instance.UserImageDirectoryPath}\\{imageFile}";
            }
            else return imageFile;
        }

        public override IEnumerable<UserDisplayData> Execute()
        {
            IEnumerable<UserDisplayData> accounts = null;

            string query = BASE_QUERY;

            SqlFilterCriterion criterion = (SqlFilterCriterion)Filter;

            bool usesParameter = criterion.UsesParameter;

            DbParameter[] parameters = null;

            if (criterion != null)
            {
                query += $"WHERE {criterion.Evaluate()} ";

                parameters = criterion.GetParameters();

            }

            if (Ordering != null) query += string.Format("ORDER BY {0} ", Ordering.Evaluate());

            MySQLProvider provider = new MySQLProvider(DBConfiguration.ConnectionString);
            SQLCaller caller = new SQLCaller(provider);

            DbCommand command = criterion.UsesParameter ? provider.CreateCommand(query, parameters) : provider.CreateCommand(query);

            accounts = caller.Get(MapUserDisplay, command);

            return accounts;
        }
    }
}
