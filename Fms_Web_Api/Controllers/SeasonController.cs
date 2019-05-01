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
    public class SeasonController : Controller
    {
        private readonly SeasonQuery _seasonQuery = new SeasonQuery();
        private readonly GameDetailsQuery _gameQuery = new GameDetailsQuery();

        //// GET api/values/5
        //[HttpGet("{gameDetailsId}")]
        //public ActionResult<IEnumerable<Season>> Get(int gameDetailsId)
        //{
        //    return _seasonQuery.GetByGame(gameDetailsId).ToList();
        //}

        // GET api/season/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<Season> Get(int gameDetailsId)
        {
            var game = _gameQuery.Get(gameDetailsId);

            return _seasonQuery.GetByGame(gameDetailsId).ToList().Where(s => s.Id == game.CurrentSeasonId).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] Season season)
        {
            return _seasonQuery.Add(season);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] Season season)
        {
            return _seasonQuery.Update(season);
        }
    }
}
