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
    internal class ProductQuery : SqlQuery<ProductModel>
    {
        #region Constants

        private static readonly string TABLE_NAME = "Products";

        private static readonly string ID_COLUMN = "Id";

        private static readonly string IMAGEFILENAME_COLUMN = "ImageFilename";

        private static readonly string NAME_COLUMN = "Name";

        private static readonly string BRAND_COLUMN = "Brand";

        private static readonly string DESCRIPTION_COLUMN = "Description";

        private static readonly string UNITPRICE_COLUMN = "UnitPrice";

        private static readonly string QUANTITY_COLUMN = "Quantity";

        private static readonly string BASE_QUERY =
            $"SELECT {ID_COLUMN}, {IMAGEFILENAME_COLUMN}, {NAME_COLUMN}, {BRAND_COLUMN}, {DESCRIPTION_COLUMN}, {UNITPRICE_COLUMN}, {QUANTITY_COLUMN} " +
            $"FROM {TABLE_NAME}";

        #endregion

        private List<ProductModel> MapProducts(IDataReader reader)
        {
            List<ProductModel> products = new List<ProductModel>();

            using (reader)
            {
                int idColumn = reader.GetOrdinal(ID_COLUMN);
                int imagefilenameColumn = reader.GetOrdinal(IMAGEFILENAME_COLUMN);
                int nameColumn = reader.GetOrdinal(NAME_COLUMN);
                int brandColumn = reader.GetOrdinal(BRAND_COLUMN);
                int descriptionColumn = reader.GetOrdinal(DESCRIPTION_COLUMN);
                int unitpriceColumn = reader.GetOrdinal(UNITPRICE_COLUMN);
                int quantityColumn = reader.GetOrdinal(QUANTITY_COLUMN);

                while (reader.Read())
                {
                    products.Add(
                        ProductModel.Existing(reader.GetInt64(idColumn),
                        reader.GetString(imagefilenameColumn),
                        reader.GetString(nameColumn),
                        reader.GetString(brandColumn),
                        reader.GetString(descriptionColumn),
                        reader.GetDouble(unitpriceColumn),
                        reader.GetInt32(quantityColumn));

                }
            }

            return products;
        }

        public override IEnumerable<ProductModel> Execute()
        {
            IEnumerable<ProductModel> products = null;

            string query = BASE_QUERY;

            SqlFilterCriterion criterion = (SqlFilterCriterion)Filter;

            bool usesParameter = criterion.UsesParameter;

            DbParameter[] parameters = null;

            if(criterion != null)
            {
                query = $"{BASE_QUERY} WHERE {criterion.Evaluate()} ";

                parameters = criterion.GetParameters();

            }

            if (Ordering != null) query += string.Format("ORDER BY {0} ", Ordering.Evaluate());

            MySQLProvider provider = new MySQLProvider(DBConfiguration.ConnectionString);
            SQLCaller caller = new SQLCaller(provider);

            DbCommand command = criterion.UsesParameter ? provider.CreateCommand(query, parameters) : provider.CreateCommand(query);
           
            products = caller.Get(MapProducts, command);

            return products;
        }

        public class IdFilter : SqlFilterCriterion
        {

            public long Id { get; set; }

            public IdFilter() { }

            public IdFilter(long id) => Id = id; 

            protected internal override string GetSQLClause() =>  $"{ID_COLUMN} = {Id}";

        }
    }
}
