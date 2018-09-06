using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal interface ISQLProvider
    {
        string ConnectionString { get; }

        DbConnection CreateConnection();
        DbConnection CreateOpenedConnection();

        DbCommand CreateCommand(string commandString, DbParameter[] inputParams = null, DbParameter[] outputParams = null);
        DbCommand CreateCommandSP(string storedProcedure, DbParameter[] inputParams = null, DbParameter[] outputParams = null);

        DbDataReader CreateReader(IDbCommand command);

        DbParameter CreateInputParameter(string parameterName, object value);

        DbParameter CreateOutputParameter(string parameterName);

        DbParameter CreateReturnParameter();

        DbParameter[] CreateInputParameters(Dictionary<string, object> source);
        DbParameter[] CreateOutputParameters(string[] source);
    }
}
