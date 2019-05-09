using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerQuery _PlayerQuery = new PlayerQuery();

        [HttpGet("{teamId}/{gameDetailsId}")]
        public ActionResult<IEnumerable<Player>> GetPlayersForTeam(int teamId, int gameDetailsId)
        {
            return _PlayerQuery.GetAllByTeam(teamId).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayersForGame(int gameDetailsId)
        {
            return _PlayerQuery.GetByGame(gameDetailsId).ToList();
        }

        // GET api/player/5
        [HttpGet("{id}")]
        public ActionResult<Player> Get(int id)
        {
            return _PlayerQuery.Get(id);
        }

        // POST api/player
        [HttpPost]
        public int Post([FromBody] Player Player)
        {
            return _PlayerQuery.Add(Player);
        }

        // PUT api/player
        [HttpPut]
        public int Put([FromBody] Player Player)
        {
            return _PlayerQuery.Update(Player);
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
