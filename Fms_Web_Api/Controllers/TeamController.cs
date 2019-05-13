using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET api/team?id=5
        [HttpGet]
        public ActionResult<Team> Get(int id)
        {
            return _teamService.Get(id);
        }

        // GET api/team/12
        [HttpGet("{gameDetailsid}")]
        public ActionResult<IEnumerable<Team>> GetTeamsForGame(int gameDetailsId)
        {
            return _teamService.GetTeamsForGame(gameDetailsId);
        }

        // POST api/team
        [HttpPost]
        public int Post([FromBody] Team team)
        {
            return _teamService.Add(team);
        }

        // PUT api/team/
        [HttpPut]
        public int Put([FromBody] Team team)
        {
            return _teamService.Update(team);
        }

        //// DELETE api/team/5
        //[HttpDelete]
        //public void Delete(int id)
        //{
        //    _teamService.Delete(id);
        //}
    }
}
