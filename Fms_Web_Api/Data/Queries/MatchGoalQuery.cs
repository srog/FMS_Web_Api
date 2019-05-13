using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Queries
{
    public class MatchGoalQuery : Query, IMatchGoalQuery
    {
        private const string GET_ALL = "spGetMatchGoals";
        private const string GET = "spGetMatchGoalById";
        private const string INSERT = "spInsertMatchGoal";

        public MatchGoal Get(int id)
        {
            return GetSingle<MatchGoal>(GET, id);
        }

        public IEnumerable<MatchGoal> GetAllForMatch(int matchId)
        {
            return GetAll<MatchGoal>(GET_ALL, new {matchId});
        }

        public int Insert(MatchGoal matchGoal)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    { "matchId", matchGoal.MatchId },
                    { "teamId", matchGoal.TeamId },
                    { "minute", matchGoal.Minute },
                    { "playerId", matchGoal.PlayerId },
                    { "assistPlayerId", matchGoal.AssistPlayerId },
                    { "ownGoal", matchGoal.OwnGoal }
                });
        }
    }
}
