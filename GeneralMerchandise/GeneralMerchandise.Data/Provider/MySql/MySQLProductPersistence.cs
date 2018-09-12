using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralMerchandise.Data.Model;

namespace GeneralMerchandise.Data.Provider.MySql
{
    internal class MySQLProductPersistence : ProductPersistence
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

        private static readonly string INSERT_STATEMENT = 
            $"INSERT INTO {TABLE_NAME} ({ID_COLUMN}, {IMAGEFILENAME_COLUMN}, {NAME_COLUMN}, {BRAND_COLUMN}, {DESCRIPTION_COLUMN}, {UNITPRICE_COLUMN}, {QUANTITY_COLUMN}) " +
            $"VALUES (@{ID_COLUMN}, @{IMAGEFILENAME_COLUMN}, @{NAME_COLUMN}, @{BRAND_COLUMN}, @{DESCRIPTION_COLUMN}, @{UNITPRICE_COLUMN}, @{QUANTITY_COLUMN})";

        private static readonly string UPDATE_STATEMENT = 
            $"UPDATE {TABLE_NAME} " +
            $"SET {IMAGEFILENAME_COLUMN} = @{IMAGEFILENAME_COLUMN}, {NAME_COLUMN} = @{NAME_COLUMN}, {BRAND_COLUMN} = @{BRAND_COLUMN}, {DESCRIPTION_COLUMN} = @{DESCRIPTION_COLUMN}, {UNITPRICE_COLUMN} = @{UNITPRICE_COLUMN}, {QUANTITY_COLUMN} = @{QUANTITY_COLUMN} " +
            $"WHERE {ID_COLUMN} = @{ID_COLUMN} ";

        private static readonly string DELETE_STATEMENT = 
            $"DELETE FROM {TABLE_NAME} " +
            $"WHERE {ID_COLUMN} = @{ID_COLUMN} ";

        #endregion

        private readonly ISQLProvider sqlFactory = new MySQLProvider(DBConfiguration.ConnectionString);

        public override void Save(ProductModel model)
        {
            SQLCaller caller = new SQLCaller(sqlFactory);

            DbConnection connection = sqlFactory.CreateConnection();

            DbParameter[] inputParameters = sqlFactory.CreateInputParameters(new Dictionary<string, object>()
            {
                { ID_COLUMN, model.Identity },
                { IMAGEFILENAME_COLUMN, model.ImageFilename },
                { NAME_COLUMN, model.Name },
                { BRAND_COLUMN, model.Brand },
                { DESCRIPTION_COLUMN, model.Description },
                { UNITPRICE_COLUMN, model.UnitPrice },
                { QUANTITY_COLUMN, model.Quantity },
            });

            DbCommand command = sqlFactory.CreateCommand(INSERT_STATEMENT, inputParameters);

            caller.ExecuteNonQuery(command);

        }

        public override void Edit(ProductModel model)
        {
            SQLCaller caller = new SQLCaller(sqlFactory);

            DbConnection connection = sqlFactory.CreateConnection();

            DbParameter[] inputParameters = sqlFactory.CreateInputParameters(new Dictionary<string, object>()
            {
                { IMAGEFILENAME_COLUMN, model.ImageFilename },
                { NAME_COLUMN, model.Name },
                { BRAND_COLUMN, model.Brand },
                { DESCRIPTION_COLUMN, model.Description },
                { UNITPRICE_COLUMN, model.UnitPrice },
                { QUANTITY_COLUMN, model.Quantity },
                { ID_COLUMN, model.Identity }
            });

            DbCommand command = sqlFactory.CreateCommand(UPDATE_STATEMENT, inputParameters);

            caller.ExecuteNonQuery(command);
        }

        public override void Delete(ProductModel model)
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
