using Fms_Web_Api.Models;
using System.Collections.Generic;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface IMatchQuery
    {
        IEnumerable<Match> GetAll(Match match);
        Match Get(int id);
        int Insert(Match match);
        int Update(Match match);
    }
}
