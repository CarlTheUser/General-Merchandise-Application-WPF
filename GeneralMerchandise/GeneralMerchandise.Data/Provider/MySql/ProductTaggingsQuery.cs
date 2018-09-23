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
    internal class ProductTaggingsQuery : SqlQuery<TagModel>
    {
        #region Constants

        private static readonly string PRODUCTTAGGINGS_TABLE_NAME = "ProductTaggings";

        private static readonly string PRODUCTTAGS_TABLE_NAME = "ProductTags";

        private static readonly string PRODUCTS_TABLE_NAME = "Products";

        private static readonly string TAGID_COLUMN = "Id";

        private static readonly string PRODUCTID_COLUMN = "ProductId";

        private static readonly string NAME_COLUMN = "Name";

        private static readonly string BASE_QUERY =
            $"SELECT T.{TAGID_COLUMN}, T.{NAME_COLUMN} " +
            $"FROM {PRODUCTTAGGINGS_TABLE_NAME} PT " +
            $"JOIN {PRODUCTTAGS_TABLE_NAME} T ON T.Id = PT.{TAGID_COLUMN} " +
            $"JOIN {PRODUCTS_TABLE_NAME} P ON P.Id = PT.{PRODUCTID_COLUMN} ";


        public static SqlOrderCriterion OrderByIdAsc() { return new SqlQuery<TagModel>.SqlOrderCriterion(TAGID_COLUMN); }

        public static SqlOrderCriterion OrderByIdDesc() { return new SqlQuery<TagModel>.SqlOrderCriterion(TAGID_COLUMN, SqlQuery<TagModel>.SqlOrderCriterion.OrderOptions.Descensding); }

        public static SqlOrderCriterion OrderByNameAsc() { return new SqlQuery<TagModel>.SqlOrderCriterion(NAME_COLUMN); }

        public static SqlOrderCriterion OrderByNameDesc() { return new SqlQuery<TagModel>.SqlOrderCriterion(NAME_COLUMN, SqlQuery<TagModel>.SqlOrderCriterion.OrderOptions.Descensding); }

        #endregion

        private List<TagModel> MapTags(IDataReader reader)
        {
            List<TagModel> tags = new List<TagModel>();

            using (reader)
            {
                int idColumn = reader.GetOrdinal(TAGID_COLUMN);
                int nameColumn = reader.GetOrdinal(NAME_COLUMN);

                while (reader.Read())
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

            if (Ordering != null) query += string.Format("ORDER BY {0} ", Ordering.Evaluate());

            MySQLProvider provider = new MySQLProvider(DBConfiguration.ConnectionString);
            SQLCaller caller = new SQLCaller(provider);

            DbCommand command = hasParameter ? provider.CreateCommand(query, parameters.ToArray()) : provider.CreateCommand(query);

            tags = caller.Get(MapTags, command);

            return tags;
        }

        //Filter to get Tags associated with a product
        public class ProductFilter : SqlFilterCriterion
        {
            public ProductModel Product { get; set; }

            public ProductFilter() { }

            public ProductFilter(ProductModel product) { Product = product; }

            public override DbParameter[] GetParameters()
            {
                throw new NotImplementedException();
            }

            protected override string GetSQLClause()
            {
                return $"P.{PRODUCTID_COLUMN} = {Product.Identity} ";
            }
        }
    }
}
