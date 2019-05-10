using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : Controller
    {
        private IMatchQuery _matchQuery { get; }

        public MatchController(IMatchQuery matchQuery)
        {
            _matchQuery = matchQuery;
        }

        // GET api/match/5
        [HttpGet]
        public ActionResult<Match> Get(int id)
        {
            return _matchQuery.Get(id);
        }

        // GET api/match/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<Match>> GetAll(int gameDetailsId)
        {
            return _matchQuery.GetAll(new Match { GameDetailsId = gameDetailsId }).ToList();
        }
        // GET api/match/5/2
        [HttpGet("{gameDetailsId}/{seasonId}")]
        public ActionResult<IEnumerable<Match>> GetAllWithSeason(int gameDetailsId, int seasonId)
        {
            return _matchQuery.GetAll(new Match { GameDetailsId = gameDetailsId, SeasonId = seasonId }).ToList();
        }
        // GET api/match/5/2/3
        [HttpGet("{gameDetailsId}/{seasonId}/{divisionId}")]
        public ActionResult<IEnumerable<Match>> GetAllWithDivision(int gameDetailsId, int seasonId, int divisionId)
        {
            return _matchQuery.GetAll(new Match { GameDetailsId = gameDetailsId, SeasonId = seasonId, DivisionId = divisionId }).ToList();
        }

        //// GET api/match/5/2/3/9
        [HttpGet("{gameDetailsId}/{seasonId}/{divisionId}/{week}")]
        public ActionResult<IEnumerable<Match>> GetAllWithWeek(int gameDetailsId, int seasonId, int divisionId, int week)
        {
            return _matchQuery.GetAll(new Match { GameDetailsId = gameDetailsId, SeasonId = seasonId, DivisionId = divisionId, Week = week }).ToList();
        }

        [HttpPost]
        public int Add([FromBody] Match match)
        {
            return _matchQuery.Insert(match);
        }

        [HttpPut]
        public int Update([FromBody] Match match)
        {
            return _matchQuery.Update(match);
        }
    }
}
