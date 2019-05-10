using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private IPlayerStatsQuery _playerStatsQuery { get; }

        public PlayerStatsController(IPlayerStatsQuery playerStatsQuery)
        {
            _playerStatsQuery = playerStatsQuery;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<PlayerStats> GetStatsForPlayer(int id)
        {
            return _playerStatsQuery.Get(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] PlayerStats PlayerStats)
        {
            return _playerStatsQuery.Add(PlayerStats);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] PlayerStats PlayerStats)
        {
            return _playerStatsQuery.Update(PlayerStats);
        }
    }
}
