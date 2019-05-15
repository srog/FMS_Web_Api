using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class MatchEventService : IMatchEventService
    {
        private readonly IMatchEventQuery _matchEventQuery;

        public MatchEventService(IMatchEventQuery matchEventQuery)
        {
            _matchEventQuery = matchEventQuery;
        }

        #region Implementation of IMatchEventService

        public List<MatchEvent> GetForMatch(int matchId)
        {
            return _matchEventQuery.GetAllForMatch(matchId).ToList();
        }

        public int Insert(MatchEvent matchEvent)
        {
            return _matchEventQuery.Insert(matchEvent);
        }


        #endregion
    }
}