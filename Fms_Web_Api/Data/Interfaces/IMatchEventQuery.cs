using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface IMatchEventQuery
    {
        IEnumerable<MatchEvent> GetAllForMatch(int matchId);
        MatchEvent Get(int id);
        int Insert(MatchEvent matchEvent);
    }
}