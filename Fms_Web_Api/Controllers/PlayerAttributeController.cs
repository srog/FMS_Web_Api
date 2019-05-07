using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerAttributeController : ControllerBase
    {
        private readonly PlayerAttributeQuery _PlayerAttributeQuery = new PlayerAttributeQuery();

        // GET api/attribute/5
        [HttpGet("{id}")]
        public ActionResult<PlayerAttribute> Get(int id)
        {
            return _PlayerAttributeQuery.Get(id);
        }

        // GET api/values/5
        [HttpGet]
        public ActionResult<IEnumerable<PlayerAttribute>> GetByPlayer(int playerId)
        {
            return _PlayerAttributeQuery.GetByPlayer(playerId).ToList();
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] PlayerAttribute playerAttribute)
        {
            return _PlayerAttributeQuery.Add(playerAttribute);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] PlayerAttribute playerAttribute)
        {
            return _PlayerAttributeQuery.Update(playerAttribute);
        }
    }
}
