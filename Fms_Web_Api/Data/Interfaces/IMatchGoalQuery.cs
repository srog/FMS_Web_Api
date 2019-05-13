using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface IMatchGoalQuery
    {
        IEnumerable<MatchGoal> GetAllForMatch(int matchId);
        MatchGoal Get(int id);
        int Insert(MatchGoal matchGoal);
    }
}
