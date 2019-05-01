using System.Collections.Generic;

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

        public T GetSingle<T>(string storedProcedureName, int id)
        {
            return _dataAccessor.QuerySingle<T>(storedProcedureName, new { id });
        }

        public int Add<T>(string storedProcedureName, object param)
        {
            return _dataAccessor.QuerySingle<int>(storedProcedureName, param);
        }

        public int Update<T>(string storedProcedureName, object param)
        {
            return _dataAccessor.Execute(storedProcedureName, param);
        }

        public int Delete<T>(string storedProcedureName, int id)
        {
            return _dataAccessor.Execute(storedProcedureName, new { id });
        }

        #endregion
    }
}
