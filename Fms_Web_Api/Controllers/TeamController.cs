using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamQuery _teamQuery = new TeamQuery();

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Team>> Get()
        {
            return _teamQuery.GetAll().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Team> Get(int id)
        {
            return _teamQuery.Get(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] Team team)
        {
            return _teamQuery.Add(team);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] Team team)
        {
            return _teamQuery.Update(team);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _teamQuery.Delete(id);
        }
    }
}
