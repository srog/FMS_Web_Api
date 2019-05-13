using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private IPlayerStatsService _playerStatsService { get; }

        public PlayerStatsController(IPlayerStatsService playerStatsService)
        {
            _playerStatsService = playerStatsService;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<PlayerStats> GetStatsForPlayer(int id)
        {
            return _playerStatsService.Get(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] PlayerStats PlayerStats)
        {
            return _playerStatsService.Add(PlayerStats);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] PlayerStats PlayerStats)
        {
            return _playerStatsService.Update(PlayerStats);
        }
    }
}
