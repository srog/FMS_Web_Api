using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : Controller
    {
        private ISeasonQuery _seasonQuery { get; }

        public SeasonController(ISeasonQuery seasonQuery)
        {
            _seasonQuery = seasonQuery;
        }

        // GET api/season?id=5
        [HttpGet]
        public ActionResult<Season> Get(int id)
        {
            return _seasonQuery.Get(id);
        }

        // GET api/season/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<Season>> GetAllForGame(int gameDetailsId)
        {
            return _seasonQuery.GetByGame(gameDetailsId).ToList();
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
