using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : Controller
    {
        private ISeasonService _seasonService { get; }

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        // GET api/season?id=5
        [HttpGet]
        public ActionResult<Season> Get(int id)
        {
            return _seasonService.Get(id);
        }

        // GET api/season/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<Season>> GetAllForGame(int gameDetailsId)
        {
            return _seasonService.GetByGame(gameDetailsId).ToList();
        }
        
        // POST api/values
        [HttpPost]
        public int Post([FromBody] Season season)
        {
            return _seasonService.Add(season);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] Season season)
        {
            return _seasonService.Update(season);
        }
    }
}
