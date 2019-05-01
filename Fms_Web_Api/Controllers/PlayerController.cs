using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerQuery _PlayerQuery = new PlayerQuery();

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Player>> Get()
        {
            return _PlayerQuery.GetAll().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Player> Get(int id)
        {
            return _PlayerQuery.Get(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] Player Player)
        {
            return _PlayerQuery.Add(Player);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] Player Player)
        {
            return _PlayerQuery.Update(Player);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _PlayerQuery.Delete(id);
        }
    }
}
