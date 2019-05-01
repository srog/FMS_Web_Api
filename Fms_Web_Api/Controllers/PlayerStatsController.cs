using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private readonly PlayerStatsQuery _PlayerStatsQuery = new PlayerStatsQuery();

        // GET api/values/5
        [HttpGet("{playerId}")]
        public ActionResult<PlayerStats> Get(int playerId)
        {
            return _PlayerStatsQuery.Get(playerId);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] PlayerStats PlayerStats)
        {
            return _PlayerStatsQuery.Add(PlayerStats);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] PlayerStats PlayerStats)
        {
            return _PlayerStatsQuery.Update(PlayerStats);
        }
    }
}
