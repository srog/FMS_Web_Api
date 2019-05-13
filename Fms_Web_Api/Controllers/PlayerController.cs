using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private IPlayerService _playerService { get; }

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET api/player/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<Player>> GetAll(int gameDetailsId)
        {
            return _playerService.GetAllPlayersInGame(gameDetailsId);
        }

        // GET api/player/1/23
        [HttpGet("{gameDetailsId}/{teamId}")]
        public ActionResult<IEnumerable<Player>> GetTeamSquad(int gameDetailsId, int teamId)
        {
            return _playerService.GetTeamSquad(teamId);
        }

        // GET api/player?id=5
        [HttpGet]
        public ActionResult<Player> Get(int id)
        {
            return _playerService.Get(id);
        }

        // POST api/player
        [HttpPost]
        public int Post([FromBody] Player Player)
        {
            return _playerService.Add(Player);
        }

        // PUT api/player
        [HttpPut]
        public int Put([FromBody] Player Player)
        {
            return _playerService.Update(Player);
        }

        // Experimental
        //[Route("player/retire?id=0")]
        //[HttpPut("{id}")]
        //public int Put(int id)
        //{
        //    return _PlayerQuery.Retire(id);
        //}

    }
}
