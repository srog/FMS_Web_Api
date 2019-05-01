using System.Collections.Generic;

namespace Fms_Web_Api.Data
{
    public interface IQuery
    {
        IEnumerable<T> GetAll<T>(string storedProcedureName);
        T GetSingle<T>(string storedProcedureName, int id);
        int Add<T>(string storedProcedureName, object param);
        int Update<T>(string storedProcedureName, object param);
        int Delete<T>(string storedProcedureName, int id);
    }
}