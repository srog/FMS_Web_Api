using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface ISeasonQuery
    {
        IEnumerable<Season> GetByGame(int gameDetailsId);
        Season Get(int id);
        int Add(Season season);
        int Update(Season season);
        int Delete(int gameDetailsid);

    }
}