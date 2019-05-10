using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private INewsQuery _newsQuery { get; }

        public NewsController(INewsQuery newsQuery)
        {
            _newsQuery = newsQuery;
        }

        //// GET api/news?id=5
        //[HttpGet("{id})")]
        //public ActionResult<News> Get(int id)
        //{
        //    return _newsQuery.Get(id);
        //}

        // GET api/news?teamid=51
        [HttpGet]
        public ActionResult<IEnumerable<News>> GetForTeam(int teamId)
        {
            return _newsQuery.GetAll(new News { TeamId = teamId }).ToList();
        }

        // GET api/news/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<News>> GetAll(int gameDetailsId)
        {
            return _newsQuery.GetAll(new News { GameDetailsId = gameDetailsId }).ToList();
        }
        // GET api/news/5/2
        [HttpGet("{gameDetailsId}/{seasonId}")]
        public ActionResult<IEnumerable<News>> GetAllWithSeason(int gameDetailsId, int seasonId)
        {
            return _newsQuery.GetAll(new News { GameDetailsId = gameDetailsId, SeasonId = seasonId }).ToList();
        }
        // GET api/news/5/2/3
        [HttpGet("{gameDetailsId}/{seasonId}/{divisionId}")]
        public ActionResult<IEnumerable<News>> GetAllWithDivision(int gameDetailsId, int seasonId, int divisionId)
        {
            return _newsQuery.GetAll(new News { GameDetailsId = gameDetailsId, SeasonId = seasonId, DivisionId = divisionId }).ToList();
        }

        //// GET api/news/5/2/3/9
        [HttpGet("{gameDetailsId}/{seasonId}/{divisionId}/{week}")]
        public ActionResult<IEnumerable<News>> GetAllWithWeek(int gameDetailsId, int seasonId, int divisionId, int week)
        {
            return _newsQuery.GetAll(new News { GameDetailsId = gameDetailsId, SeasonId = seasonId, DivisionId = divisionId, Week = week }).ToList();
        }
        
        // POST api/player
        [HttpPost]
        public int Post([FromBody] News news)
        {
            return _newsQuery.Add(news);
        }

    }
}
