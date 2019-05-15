using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchEventController : Controller
    {
        private IMatchEventService _matchEventService { get; }

        public MatchEventController(IMatchEventService matchEventService)
        {
            _matchEventService = matchEventService;
        }

        // GET api/matchevent/5
        [HttpGet("{matchId}")]
        public ActionResult<IEnumerable<MatchEvent>> GetForMatch(int matchId)
        {
            return _matchEventService.GetForMatch(matchId).ToList();
        }
    }
}