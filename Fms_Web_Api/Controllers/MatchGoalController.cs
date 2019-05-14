using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchGoalController : Controller
    {
        private IMatchGoalService _matchGoalService { get; }

        public MatchGoalController(IMatchGoalService matchGoalService)
        {
            _matchGoalService = matchGoalService;
        }

        // GET api/match/5
        [HttpGet("{matchId}")]
        public ActionResult<IEnumerable<MatchGoal>> GetForMatch(int matchId)
        {
            return _matchGoalService.GetForMatch(matchId).ToList();
        }
    }
}