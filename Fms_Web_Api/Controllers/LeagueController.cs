using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : Controller
    {
        private readonly TeamSeasonQuery _teamSeasonQuery = new TeamSeasonQuery();
     
        // GET api/player/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<TeamSeason>> Get(int gameDetailsId, int divisionId)
        {
            return _teamSeasonQuery.GetByGameAndDivision(gameDetailsId, divisionId).ToList();
        }

        // GET api/player/5
        [HttpGet("{gameDetailsId}/{seasonId}")]
        public ActionResult<IEnumerable<TeamSeason>> GetByGameSeasonAndDivision(int gameDetailsId, int divisionId, int seasonId)
        {
            return _teamSeasonQuery.GetByGameSeasonAndDivision(gameDetailsId, divisionId, seasonId).ToList();
        }

    }
}
