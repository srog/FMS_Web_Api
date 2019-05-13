using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : Controller
    {
        private ITeamSeasonService _teamSeasonService { get; }

        public LeagueController(ITeamSeasonService teamSeasonService)
        {
            _teamSeasonService = teamSeasonService;
        }

        // GET api/league
        [HttpGet("{gameDetailsId}/{seasonId}/{divisionId}")]
        public ActionResult<IEnumerable<TeamSeason>> GetByGameSeasonAndDivision(int gameDetailsId, int divisionId, int seasonId)
        {
            return _teamSeasonService.GetByGameSeasonAndDivision(gameDetailsId, divisionId, seasonId)
                .OrderBy(ts => ts.Position)
                .ToList();
        }

        // POST api/league
        [HttpPost]
        public ActionResult<int> EndOfSeasonUpdate([FromBody] EndOfSeasonData endOfSeasonData)
        {
            return _teamSeasonService.CreateForNewSeason(endOfSeasonData.GameDetailsId, endOfSeasonData.OldSeasonId, endOfSeasonData.NewSeasonId);

        }

    }
}
