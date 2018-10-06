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
    internal class TagQuery : SqlQuery<TagModel>
    {

        #region Constants

        private static readonly string TABLE_NAME = "ProductTags";

        private static readonly string ID_COLUMN = "Id";

        private static readonly string NAME_COLUMN = "Name";

        private static readonly string BASE_QUERY = 
            $"SELECT {ID_COLUMN}, {NAME_COLUMN} " +
            $"FROM {TABLE_NAME} ";

        public static SqlOrderCriterion OrderByIdAsc() { return new SqlQuery<TagModel>.SqlOrderCriterion(ID_COLUMN); }

        public static SqlOrderCriterion OrderByIdDesc() { return new SqlQuery<TagModel>.SqlOrderCriterion(ID_COLUMN, SqlQuery<TagModel>.SqlOrderCriterion.OrderOptions.Descensding); }

        public static SqlOrderCriterion OrderByNameAsc() { return new SqlQuery<TagModel>.SqlOrderCriterion(NAME_COLUMN); }

        public static SqlOrderCriterion OrderByNameDesc() { return new SqlQuery<TagModel>.SqlOrderCriterion(NAME_COLUMN, SqlQuery<TagModel>.SqlOrderCriterion.OrderOptions.Descensding); }

        #endregion

        private List<TagModel> MapTags(IDataReader reader)
        {
            List<TagModel> tags = new List<TagModel>();

            using (reader)
            {
                int idColumn = reader.GetOrdinal(ID_COLUMN);
                int nameColumn = reader.GetOrdinal(NAME_COLUMN);

                while(reader.Read())
                {
                    tags.Add(TagModel.Existing(
                        reader.GetInt32(idColumn),
                        reader.GetString(nameColumn))
                        );
                }
            }

            return tags;
        }

        public override IEnumerable<TagModel> Execute()
        {
            IEnumerable<TagModel> tags = null;

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

            tags = caller.Get(MapTags, command);

            return tags;
        }
        
        public class IdFilter : SqlFilterCriterion
        {

            int Id{ get; set; }

            public IdFilter() { }

            public IdFilter(int id) { Id = id; }

            public override DbParameter[] GetParameters()
            {
                return new DbParameter[] { };
            }

            protected internal override string GetSQLClause()
            {
                return $"{ID_COLUMN} = {Id} ";
            }
        }

        public class NameFilter : SqlFilterCriterion
        {
            public string Name { get; set; }

            public NameFilter() { UsesParameter = true; }

            public NameFilter(string name) : this() { Name = name; }

            public override DbParameter[] GetParameters()
            {
                return new DbParameter[] { new MySQLProvider().CreateInputParameter($"@{NAME_COLUMN}", Name) };
            }

            protected internal override string GetSQLClause()
            {
                return $"{NAME_COLUMN} = @{NAME_COLUMN} ";
            }
        }
    }
}
