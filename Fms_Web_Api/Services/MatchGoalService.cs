using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class MatchGoalService : IMatchGoalService
    {
        private readonly IMatchGoalQuery _matchGoalQuery;

        public MatchGoalService(IMatchGoalQuery matchGoalQuery)
        {
            _matchGoalQuery = matchGoalQuery;
        }

        public List<MatchGoal> GetForMatch(int matchId)
        {
            return _matchGoalQuery.GetAllForMatch(matchId).ToList();
        }
    }
}