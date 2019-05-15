using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IMatchEventService
    {
        List<MatchEvent> GetForMatch(int matchId);
        int Insert(MatchEvent matchEvent);
    }
}