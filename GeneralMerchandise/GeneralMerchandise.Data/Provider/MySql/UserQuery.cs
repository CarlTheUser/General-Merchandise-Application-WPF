using GeneralMerchandise.Common.Type;
using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class UserQuery : SqlQuery<UserModel>
    {

        #region Constants

        private static readonly string TABLE_NAME = "UserInfo";

        private static readonly string ID_COLUMN = "Id";

        private static readonly string IMAGEFILENAME_COLUMN = "ImageFilename";

        private static readonly string FIRSTNAME_COLUMN = "Firstname";

        private static readonly string MIDDLENAME_COLUMN = "MiIddlename";

        private static readonly string LASTNAME_COLUMN = "Lastname";

        private static readonly string GENDER_COLUMN = "Gender";

        private static readonly string BIRTHDATE_COLUMN = "Birthdate";

        private static readonly string CONTACTNUMBER_COLUMN = "ContactNumber";

        private static readonly string EMAIL_COLUMN = "Email";

        private static readonly string ADDRESS_COLUMN = "Address";

        private static readonly string BASE_QUERY =
            string.Format("SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} FROM {10} ",
                ID_COLUMN,
                IMAGEFILENAME_COLUMN,
                FIRSTNAME_COLUMN,
                MIDDLENAME_COLUMN,
                LASTNAME_COLUMN,
                GENDER_COLUMN,
                BIRTHDATE_COLUMN,
                CONTACTNUMBER_COLUMN,
                EMAIL_COLUMN,
                ADDRESS_COLUMN,
                TABLE_NAME);

        #endregion

        private List<UserModel> MapUsers(IDataReader reader)
        {
            List<UserModel> users = new List<UserModel>();

            using (reader)
            {
                int idColumn = reader.GetOrdinal(ID_COLUMN);
                int imageFilenameColumn = reader.GetOrdinal(IMAGEFILENAME_COLUMN);
                int firstnameColumn = reader.GetOrdinal(FIRSTNAME_COLUMN);
                int middlenameColumn = reader.GetOrdinal(MIDDLENAME_COLUMN);
                int lastnameColumn = reader.GetOrdinal(LASTNAME_COLUMN);
                int genderColumn = reader.GetOrdinal(GENDER_COLUMN);
                int birthdateColumn = reader.GetOrdinal(BIRTHDATE_COLUMN);
                int contactNumberColumn = reader.GetOrdinal(CONTACTNUMBER_COLUMN);
                int emailColumn = reader.GetOrdinal(EMAIL_COLUMN);
                int addressColumn = reader.GetOrdinal(ADDRESS_COLUMN);

                while(reader.Read())
                {
                    users.Add(UserModel.FromDB(
                        reader.GetInt32(idColumn),
                        reader.GetString(imageFilenameColumn),
                        reader.GetString(firstnameColumn),
                        reader.GetString(middlenameColumn),
                        reader.GetString(lastnameColumn),
                        GetGender(reader.GetValue(genderColumn)),
                        reader.GetDateTime(birthdateColumn),
                        reader.GetString(contactNumberColumn),
                        reader.GetString(emailColumn),
                        reader.GetString(addressColumn)
                        ));
                }
            }

            return users;
        }

        private Gender GetGender(object o)
        {
            if (o != DBNull.Value)
            {
                return (Gender)(int)o;
            }
            return Gender.Unknown;
        }

        public override IEnumerable<UserModel> Execute()
        {
            IEnumerable<UserModel> users = null;

            string query = BASE_QUERY;

            SqlFilterCriterion criterion = (SqlFilterCriterion)Filter;

            List<DbParameter> parameters = new List<DbParameter>();

            bool hasParameter = false;

            if (criterion != null)
            {
                query += string.Format("WHERE {0} ", criterion.Evaluate());

                if (criterion.UsesParameter)
                {
                    parameters.AddRange(criterion.GetParameters());
                    hasParameter = true;
                }

                while (criterion.HasNextCriteria)
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

            if (Grouping != null) query += string.Format("GROUP BY {0} ", Grouping.Evaluate());

            if (Ordering != null) query += string.Format("ORDER BY {0} ", Ordering.Evaluate());

            MySQLProvider provider = new MySQLProvider(DBConfiguration.ConnectionString);
            SQLCaller caller = new SQLCaller(provider);

            DbCommand command = hasParameter ? provider.CreateCommand(query, parameters.ToArray()) : provider.CreateCommand(query);

            users = caller.Get(MapUsers, command);

            return users;
        }

        public class IdFilter : SqlFilterCriterion
        {
            public int Id { get; set; }

            public IdFilter() { }

            public IdFilter(int id) { Id = id; }

            public override DbParameter[] GetParameters()
            {
                return new DbParameter[] { };
            }

            protected override string GetSQLClause()
            {
                return $"{ID_COLUMN} = {Id} ";
            }
        }

        public class NameFilter : SqlFilterCriterion
        {

            public string Name { get; set; }

            public NameFilter() { UsesParameter = true; }

            public NameFilter(string name) { Name = name; }

            public override DbParameter[] GetParameters()
            {
                return new MySQLProvider().CreateInputParameters(new Dictionary<string, object>
                {
                    { FIRSTNAME_COLUMN, Name },
                    { MIDDLENAME_COLUMN, Name },
                    { LASTNAME_COLUMN, Name }
                });
            }

            protected override string GetSQLClause()
            {
                return string.Format("({0} LIKE '%'+{1}+'%' OR {2} LIKE '%'+{3}+'%' OR {4} LIKE '%'+{5}+'%') ", FIRSTNAME_COLUMN, Name, MIDDLENAME_COLUMN, Name, LASTNAME_COLUMN, Name);
            }
        }

        public class GenderFilter : SqlFilterCriterion
        {
            public Gender Gender { get; set; }

            public GenderFilter() { UsesParameter = true; }

            public GenderFilter(Gender gender) : this() { Gender = gender; }

            public override DbParameter[] GetParameters()
            {
                DbParameter parameter = new MySQLProvider().CreateInputParameter("@" + GENDER_COLUMN, (int)Gender);
                parameter.DbType = DbType.Byte;
                return new DbParameter[] { parameter };
            }

            protected override string GetSQLClause()
            {
                return GENDER_COLUMN + " = @" + GENDER_COLUMN + " ";
            }
        }
    }
}
