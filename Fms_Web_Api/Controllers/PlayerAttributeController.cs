using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerAttributeController : ControllerBase
    {
        private IPlayerAttributeQuery _playerAttributeQuery { get; }

        public PlayerAttributeController(IPlayerAttributeQuery playerAttributeQuery)
        {
            _playerAttributeQuery = playerAttributeQuery;
        }

        // GET api/playerattribute?id=5
        [HttpGet("{id}")]
        public ActionResult<PlayerAttribute> Get(int id)
        {
            return _playerAttributeQuery.Get(id);
        }

        // GET api/playerattribute?playerid=4
        [HttpGet]
        public ActionResult<IEnumerable<PlayerAttribute>> GetByPlayer(int playerId)
        {
            return _playerAttributeQuery.GetByPlayer(playerId).ToList();
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] PlayerAttribute playerAttribute)
        {
            return _playerAttributeQuery.Add(playerAttribute);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] PlayerAttribute playerAttribute)
        {
            return _playerAttributeQuery.Update(playerAttribute);
        }
    }
}
