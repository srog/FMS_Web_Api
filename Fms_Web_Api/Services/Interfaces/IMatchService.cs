using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IMatchService
    {
        Match PlayMatch(int id);
        List<Match> GetAll(Match match);
        Match Get(int id);
        int Insert(Match match);
        int Update(Match match);
    }
}