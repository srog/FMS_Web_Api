using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IMatchGoalService
    {
        List<MatchGoal> GetForMatch(int matchId);
        int Insert(MatchGoal matchGoal);
    }
}