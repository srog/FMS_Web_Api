using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Queries
{
    public class MatchEventQuery : Query, IMatchEventQuery
    {
        private const string GET_ALL = "spGetMatchEvents";
        private const string GET = "spGetMatchEventById";
        private const string INSERT = "spInsertMatchEvent";

        public MatchEvent Get(int id)
        {
            return GetSingle<MatchEvent>(GET, id);
        }

        public IEnumerable<MatchEvent> GetAllForMatch(int matchId)
        {
            return GetAll<MatchEvent>(GET_ALL, new { matchId });
        }

        public int Insert(MatchEvent matchEvent)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    { "matchId", matchEvent.MatchId },
                    { "teamId", matchEvent.TeamId },
                    { "minute", matchEvent.Minute },
                    { "playerId", matchEvent.PlayerId },
                    { "eventType", matchEvent.EventType }
                });
        }
    }
}