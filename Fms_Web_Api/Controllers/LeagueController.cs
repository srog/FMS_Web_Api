using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : Controller
    {
        private ITeamSeasonQuery _teamSeasonQuery { get; }

        public LeagueController(ITeamSeasonQuery teamSeasonQuery)
        {
            _teamSeasonQuery = teamSeasonQuery;
        }

        // GET api/league
        [HttpGet("{gameDetailsId}/{seasonId}/{divisionId}")]
        public ActionResult<IEnumerable<TeamSeason>> GetByGameSeasonAndDivision(int gameDetailsId, int divisionId, int seasonId)
        {
            return _teamSeasonQuery.GetByGameSeasonAndDivision(gameDetailsId, divisionId, seasonId).ToList();
        }

        // POST api/league
        [HttpPost]
        public ActionResult<int> EndOfSeasonUpdate([FromBody] EndOfSeasonData endOfSeasonData)
        {
            return _teamSeasonQuery.CreateForNewSeason(endOfSeasonData.GameDetailsId, endOfSeasonData.OldSeasonId, endOfSeasonData.NewSeasonId);

        }

    }
}
