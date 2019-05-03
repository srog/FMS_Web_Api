using System.Collections.Generic;

namespace Fms_Web_Api.Data
{
    public interface IQuery
    {
        IEnumerable<T> GetAll<T>(string storedProcedureName);
        IEnumerable<T> GetAllById<T>(string storedProcedureName, string parameterName, int idValue);
        T GetSingle<T>(string storedProcedureName, int id);
        T GetSingleById<T>(string storedProcedureName, string parameterName, int idValue);
        int Add(string storedProcedureName, object param);
        int Add(string storedProcedureName, Dictionary<string, object> param);
        int Update(string storedProcedureName, object param);
        int Delete(string storedProcedureName, int id);

        int UpdateSingleColumn(string storedProcedureName, string fieldName, object fieldValue);
        int ExecuteProcedure(string storedProcedureName);
    }
}