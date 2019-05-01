using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartNewGameController : ControllerBase
    {
        private readonly TeamQuery _teamQuery = new TeamQuery();
        private readonly GameDetailsQuery _gameQuery = new GameDetailsQuery();
        private readonly PlayerCreator _playerCreator = new PlayerCreator();
        
        // POST api/values
        [HttpPost]
        public void Post([FromBody] GameDetails gameDetails)
        {
            var gameId = _gameQuery.Add(gameDetails);
            _teamQuery.CreateAllTeamsForGame(gameId);
            var teamList = _teamQuery.GetByGame(gameId);

            _playerCreator.CreateAllPlayersForGame(teamList);
        }

    }
}
