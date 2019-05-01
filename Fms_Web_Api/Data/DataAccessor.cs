using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Fms_Web_Api.Data
{
    public class DataAccessor : IDataAccessor
    {
        private readonly IDbConnection _connection;

        public DataAccessor(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose() => _connection.Dispose();

        public int Execute(string storedProcName, object param = null)
        {
            return _connection.Execute(storedProcName, param, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<T> Query<T>(string storedProcName, object param = null)
        {
            return _connection.Query<T>(storedProcName, param, commandType: CommandType.StoredProcedure);
        }

        public T QuerySingle<T>(string storedProcName, object param = null)
        {
            return _connection.QuerySingle<T>(storedProcName, param, commandType: CommandType.StoredProcedure);
        }
    }
}