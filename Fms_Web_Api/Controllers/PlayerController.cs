using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private IPlayerQuery _playerQuery { get; }

        public PlayerController(IPlayerQuery playerQuery)
        {
            _playerQuery = playerQuery;
        }

        // GET api/player/5
        [HttpGet("{gameDetailsId}")]
        public ActionResult<IEnumerable<Player>> GetAll(int gameDetailsId)
        {
            return _playerQuery.GetAll(new Player { GameDetailsId = gameDetailsId }).ToList();
        }

        // GET api/player/1/23
        [HttpGet("{gameDetailsId}/{teamId}")]
        public ActionResult<IEnumerable<Player>> GetTeamSquad(int gameDetailsId, int teamId)
        {
            return _playerQuery.GetAll(new Player { GameDetailsId = gameDetailsId, TeamId = teamId }).ToList();
        }

        // GET api/player?id=5
        [HttpGet]
        public ActionResult<Player> Get(int id)
        {
            return _playerQuery.Get(id);
        }

        // POST api/player
        [HttpPost]
        public int Post([FromBody] Player Player)
        {
            return _playerQuery.Add(Player);
        }

        // PUT api/player
        [HttpPut]
        public int Put([FromBody] Player Player)
        {
            return _playerQuery.Update(Player);
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
