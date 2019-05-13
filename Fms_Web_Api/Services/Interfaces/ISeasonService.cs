using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface ISeasonService
    {
        List<Season> GetByGame(int gameDetailsId);
        Season Get(int id);
        int Add(Season season);
        int AddNew(int gameDetailsId);
        int Update(Season season);
        int Delete(int gameDetailsid);
    }
}
