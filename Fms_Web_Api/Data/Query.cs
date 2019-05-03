using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Fms_Web_Api.Data
{
    public abstract class Query  : IQuery
    {
        private readonly IDataAccessor _dataAccessor;
        protected Query()
        {
            var db = new FmsDbContext();
            _dataAccessor = new DataAccessor(db.Connection);
        }
        
        #region Implementation of IQuery

        public IEnumerable<T> GetAll<T>(string storedProcedure)
        {
            return _dataAccessor.Query<T>(storedProcedure);
        }

        public IEnumerable<T> GetAllById<T>(string storedProcedure, string parameterName, int idValue)
        {
            var param = new DynamicParameters();
            param.Add(parameterName, dbType:DbType.Int32, value:idValue);
            return _dataAccessor.Query<T>(storedProcedure, param);
        }


        public T GetSingle<T>(string storedProcedureName, int id)
        {
            return _dataAccessor.QuerySingle<T>(storedProcedureName, new { id });
        }

        public T GetSingleById<T>(string storedProcedure, string parameterName, int idValue)
        {
            var param = new DynamicParameters();
            param.Add(parameterName, dbType: DbType.Int32, value: idValue);
            return _dataAccessor.QuerySingle<T>(storedProcedure, param);
        }

        // Returns the newly created ID , not the SQL result
        public int Add(string storedProcedureName, Dictionary<string, object> param)
        {
            var allParam = new DynamicParameters();
            allParam.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            foreach (var par in param)
            {
                allParam.Add(par.Key, par.Value);
            }

            var result = _dataAccessor.QuerySingle<int>(storedProcedureName, allParam);
            if (result != 0)
            {
                return 0; // error
            }
            return allParam.Get<int>("id");
        }

        // Returns the newly created ID , not the SQL result
        public int Add(string storedProcedureName, object param)
        {
            var result = _dataAccessor.QuerySingle<int>(storedProcedureName, param);
            if (result != 0)
            {
                return 0; // error
            }
            return result;
        }

        public int Update(string storedProcedureName, object param)
        {
            return _dataAccessor.Execute(storedProcedureName, param);
        }

        public int Delete(string storedProcedureName, int id)
        {
            return _dataAccessor.Execute(storedProcedureName, new { id });
        }

        public int UpdateSingleColumn(string storedProcedureName, string fieldName, object fieldValue)
        {
            var param = new DynamicParameters();
            param.Add(fieldName, fieldValue);
            return _dataAccessor.Execute(storedProcedureName, param);
        }

        public int ExecuteProcedure(string storedProcedureName)
        {
            return _dataAccessor.Execute(storedProcedureName);
        }

        #endregion
    }
}
