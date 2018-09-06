using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Provider
{
    internal class SQLCaller
    {

        ISQLProvider sqlProvider;

        public DataTable Query(string queryString)
        {
            DataTable dt = null;

            using (DbConnection connection = sqlProvider.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = queryString;
                    try
                    {
                        connection.Open();
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            dt = new DataTable();
                            dt.Load(dr);
                        }
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return dt;
        }

        public DataTable Query(DbCommand command)
        {
            DataTable dt = null;

            using (DbConnection connection = sqlProvider.CreateConnection())
            {
                command.Connection = connection;
                try
                {
                    connection.Open();
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        dt = new DataTable();
                        dt.Load(dr);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return dt;
        }

        public int ExecuteNonQuery(DbCommand command)
        {

            int ret = 0;
            using (DbConnection connection = sqlProvider.CreateConnection())
            {
                command.Connection = connection;
                try
                {
                    connection.Open();
                    ret = command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return ret;
        }

        public int ExecuteNonQuery(string commandString)
        {
            int ret = 0;
            using (DbConnection connection = sqlProvider.CreateConnection())
            {
                DbCommand command = connection.CreateCommand();
                command.CommandText = commandString;
                try
                {
                    connection.Open();
                    ret = command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return ret;
        }

        public object ExecuteScalar(string queryString)
        {
            object obj = null;

            using (DbConnection connection = sqlProvider.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = queryString;
                    try
                    {
                        connection.Open();
                        obj = command.ExecuteScalar();
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return obj;
        }

        public object ExecuteScalar(DbCommand command)
        {
            object obj = null;

            using (DbConnection connection = sqlProvider.CreateConnection())
            {
                command.Connection = connection;
                try
                {
                    connection.Open();
                    obj = command.ExecuteScalar();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return obj;
        }

        public IEnumerable<T> Get<T>(Func<IDataReader, List<T>> mappingMethod, DbCommand command)
        {
            List<T> temp;

            if (mappingMethod == null) throw new Exception("Mapping method is null");

            using (DbConnection connection = sqlProvider.CreateConnection())
            {
                command.Connection = connection;

                try
                {
                    command.Connection.Open();

                    temp = mappingMethod.Invoke(command.ExecuteReader());
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    command.Connection.Close();
                }
            }

            return temp;
        }
    }
}
