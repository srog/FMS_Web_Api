using System.Collections.Generic;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerAttributeController : ControllerBase
    {
        private IPlayerAttributeService _playerAttributeService { get; }

        public PlayerAttributeController(IPlayerAttributeService playerAttributeService)
        {
            _playerAttributeService = playerAttributeService;
        }

        // GET api/playerattribute?id=5
        [HttpGet("{id}")]
        public ActionResult<PlayerAttribute> Get(int id)
        {
            return _playerAttributeService.Get(id);
        }

        // GET api/playerattribute?playerid=4
        [HttpGet]
        public ActionResult<IEnumerable<PlayerAttribute>> GetByPlayer(int playerId)
        {
            return _playerAttributeService.GetByPlayer(playerId);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] PlayerAttribute playerAttribute)
        {
            return _playerAttributeService.Add(playerAttribute);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] PlayerAttribute playerAttribute)
        {
            return _playerAttributeService.Update(playerAttribute);
        }
    }
}
