using System;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private readonly NewsQuery _newsQuery = new NewsQuery();

        // GET api/player/5
        [HttpGet]
        public ActionResult<IEnumerable<News>> GetAllForSeason(int seasonId)
        {
            return _newsQuery.GetAllForSeason(seasonId).ToList();
        }

        // GET api/player/5
        [HttpGet("{id})")]
        public ActionResult<News> Get(int id)
        {
            return _newsQuery.Get(id);
        }

        // GET api/player/5
        [HttpGet("{seasonId}")]
        public ActionResult<IEnumerable<News>> GetBySeasonAndDivision(int divisionId, int seasonId)
        {
            return _newsQuery.GetBySeasonAndDivision(divisionId, seasonId).ToList();
        }


        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<News>> GetLatest(int gameDetailsId)
        {
            return _newsQuery.GetLatest(gameDetailsId).ToList();
        }

        // POST api/player
        [HttpPost]
        public int Post([FromBody] News news)
        {
            return _newsQuery.Add(news);
        }

    }
}
