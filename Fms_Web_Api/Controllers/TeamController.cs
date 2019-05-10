using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ITeamQuery _teamQuery { get; }

        public TeamController(ITeamQuery teamQuery)
        {
            _teamQuery = teamQuery;
        }

        // GET api/team?id=5
        [HttpGet]
        public ActionResult<Team> Get(int id)
        {
            return _teamQuery.Get(id);
        }

        // GET api/team/12
        [HttpGet("{gameDetailsid}")]
        public ActionResult<IEnumerable<Team>> GetTeamsForGame(int gameDetailsId)
        {
            return _teamQuery.GetByGame(gameDetailsId).ToList();
        }

        // POST api/team
        [HttpPost]
        public int Post([FromBody] Team team)
        {
            return _teamQuery.Add(team);
        }

        // PUT api/team/
        [HttpPut]
        public int Put([FromBody] Team team)
        {
            return _teamQuery.Update(team);
        }

        // DELETE api/team/5
        [HttpDelete]
        public void Delete(int id)
        {
            _teamQuery.Delete(id);
        }
    }
}
