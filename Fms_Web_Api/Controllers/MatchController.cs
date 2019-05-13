using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Services.Interfaces;
using Fms_Web_Api.Utilities;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : Controller
    {
        private IMatchService _matchService { get; }
        private ITeamSeasonService _teamSeasonService { get; }

        public MatchController(ITeamSeasonService teamSeasonService, IMatchService matchService)
        {
            _teamSeasonService = teamSeasonService;
            _matchService = matchService;
        }

        // GET api/match/5
        [HttpGet]
        public ActionResult<Match> Get(int id)
        {
            return _matchService.Get(id);
        }

        // GET api/match/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<Match>> GetAll(int gameDetailsId)
        {
            return _matchService.GetAll(new Match { GameDetailsId = gameDetailsId }).ToList();
        }
        // GET api/match/5/2
        [HttpGet("{gameDetailsId}/{seasonId}")]
        public ActionResult<IEnumerable<Match>> GetAllWithSeason(int gameDetailsId, int seasonId)
        {
            return _matchService.GetAll(new Match { GameDetailsId = gameDetailsId, SeasonId = seasonId }).ToList();
        }
        // GET api/match/5/2/3
        [HttpGet("{gameDetailsId}/{seasonId}/{divisionId}")]
        public ActionResult<IEnumerable<Match>> GetAllWithDivision(int gameDetailsId, int seasonId, int divisionId)
        {
            return _matchService.GetAll(new Match { GameDetailsId = gameDetailsId, SeasonId = seasonId, DivisionId = divisionId }).ToList();
        }

        //// GET api/match/5/2/3/9
        [HttpGet("{gameDetailsId}/{seasonId}/{divisionId}/{week}")]
        public ActionResult<IEnumerable<Match>> GetAllWithWeek(int gameDetailsId, int seasonId, int divisionId, int week)
        {
            return _matchService.GetAll(new Match { GameDetailsId = gameDetailsId, SeasonId = seasonId, DivisionId = divisionId, Week = week }).ToList();
        }

        [HttpPost]
        public int Add([FromBody] Match match)
        {
            return _matchService.Insert(match);
        }

        [HttpPut]
        public int Update([FromBody] Match match)
        {
            return _matchService.Update(match);
        }

        [HttpPut("{id}")]
        public int PlaySingleMatch(int id)
        {
            var match = _matchService.PlayMatch(id);
            if (match == null)
            {
                // already played
                return 0;
            }
            var homeTeamSeason = _teamSeasonService.GetCurrentForTeam(match.HomeTeamId.Value);
            _teamSeasonService.Recalculate(homeTeamSeason.Id);
            var awayTeamSeason = _teamSeasonService.GetCurrentForTeam(match.AwayTeamId.Value);
            _teamSeasonService.Recalculate(awayTeamSeason.Id);

            _teamSeasonService.RecalculateDivisionPositions(match.SeasonId.Value, match.DivisionId.Value);

            return 1;
        }
 

        [HttpPut("{seasonId}/{week}")]
        public void PlayAllMatchesForWeek(int seasonId, int week)
        {
            4.TimesWithIndex((i) => PlayAllMatchesForDivision(seasonId, week, i));
        }

        [HttpPut("{seasonId}/{week}/{divisionId}")]
        public void PlayAllMatchesForDivision(int seasonId, int week, int divisionId)
        {
            var matchFilter = new Match
            {
                SeasonId = seasonId,
                Week = week,
                DivisionId = divisionId
            };
            var matches = _matchService.GetAll(matchFilter);

            foreach (var match in matches)
            {
                _matchService.PlayMatch(match.Id);
            }

            // Note - assuming everyone plays every week - no need to recalc all if not. 
            _teamSeasonService.RecalculateAll(seasonId, divisionId);
        }
    }
}
