using System.Collections.Generic;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDetailsController : ControllerBase
    {
        private IGameDetailsService _gameDetailsService { get; }

        public GameDetailsController(IGameDetailsService gameDetailsService)
        {
            _gameDetailsService = gameDetailsService;
        }

        // GET api/GameDetails/5
        [HttpGet("{id}")]
        public ActionResult<GameDetails> Get(int id)
        {
            return _gameDetailsService.Get(id);
        }

        // GET api/gamedetails
        [HttpGet]
        public ActionResult<IEnumerable<GameDetails>> GetAll()
        {
            return _gameDetailsService.GetAll();
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] GameDetails gameDetails)
        {
            return _gameDetailsService.Insert(gameDetails);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] GameDetails gameDetails)
        {
            return _gameDetailsService.Update(gameDetails);
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
            _gameDetailsService.Delete(id);
        }
    }
}
