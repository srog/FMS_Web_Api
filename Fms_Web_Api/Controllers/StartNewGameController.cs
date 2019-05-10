using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartNewGameController : ControllerBase
    {
        private IConfiguration Configuration { get; }
        private ITeamQuery _teamQuery { get;  }
        private IGameDetailsQuery _gameDetailsQuery { get;  }
        private IPlayerCreator _playerCreator { get; }
        private ISeasonQuery _seasonQuery { get; }
        private ITeamSeasonQuery _teamSeasonQuery { get; }
        
        public StartNewGameController(
            IConfiguration configuration,
            ITeamQuery teamQuery,
            IGameDetailsQuery gameDetailsQuery,
            IPlayerCreator playerCreator,
            ISeasonQuery seasonQuery,
            ITeamSeasonQuery teamSeasonQuery)
        {
            Configuration = configuration;
            _teamSeasonQuery = teamSeasonQuery;
            _teamQuery = teamQuery;
            _gameDetailsQuery = gameDetailsQuery;
            _playerCreator = playerCreator;
            _seasonQuery = seasonQuery;
        }


        // Create game
        // Create first season
        // Create all teams
        // Create all teamSeasons
        // Create all players

        // POST api/values
        [HttpPost]
        public int Post()
        {
            var newGame = new GameDetails {ManagerName = "", TeamId=0, CurrentSeasonId=0, CurrentWeek=0};

            var gameId = _gameDetailsQuery.Insert(newGame);
            if (gameId > 0)
            {
                newGame.Id = gameId;

                var initialSeason = new Season {GameDetailsId = newGame.Id, StartYear = Configuration.GetValue<int>("GameStartYear"), Completed = false};
                var seasonId = _seasonQuery.Add(initialSeason);

                newGame.CurrentSeasonId = seasonId;
                _gameDetailsQuery.Update(newGame);

                _teamQuery.CreateAllTeamsForGame(gameId);
                var teamList = _teamQuery.GetByGame(gameId);

                _teamSeasonQuery.CreateForNewGame(teamList, seasonId, gameId);
                _playerCreator.CreateAllPlayersForGame(teamList);
            }

            return gameId;
        }

        // Update new game with selected team
        // PUT api/StartNewGame
        [HttpPut]
        public void Put([FromBody] GameDetails gameDetails)
        {
            _gameDetailsQuery.Update(gameDetails);
        }

    }
}
