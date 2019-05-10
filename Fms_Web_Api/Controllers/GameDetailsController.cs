using System.Linq;
using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDetailsController : ControllerBase
    {
        private IGameDetailsQuery _gameDetailsQuery { get; }

        public GameDetailsController(IGameDetailsQuery gameDetailsQuery)
        {
            _gameDetailsQuery = gameDetailsQuery;
        }

        // GET api/gamedetails
        [HttpGet]
        public ActionResult<IEnumerable<GameDetails>> GetAll()
        {
            return  _gameDetailsQuery.GetAll().ToList();
        }

        // GET api/GameDetails/5
        [HttpGet("{id}")]
        public ActionResult<GameDetails> Get(int id)
        {
            return _gameDetailsQuery.Get(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] GameDetails gameDetails)
        {
            return _gameDetailsQuery.Insert(gameDetails);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] GameDetails gameDetails)
        {
            return _gameDetailsQuery.Update(gameDetails);
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
            _gameDetailsQuery.Delete(id);
        }
    }
}
