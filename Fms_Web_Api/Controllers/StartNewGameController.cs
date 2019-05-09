using Fms_Web_Api.Data;
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

        private readonly TeamQuery _teamQuery = new TeamQuery();
        private readonly GameDetailsQuery _gameQuery = new GameDetailsQuery();
        private readonly IPlayerCreator _playerCreator = new PlayerCreator();
        private readonly SeasonQuery _seasonQuery = new SeasonQuery();
        private readonly TeamSeasonQuery _teamSeasonQuery = new TeamSeasonQuery();

        public StartNewGameController(IConfiguration configuration)
        {
            Configuration = configuration;
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

            var gameId = _gameQuery.Add(newGame);
            if (gameId > 0)
            {
                newGame.Id = gameId;

                var initialSeason = new Season {GameDetailsId = newGame.Id, StartYear = Configuration.GetValue<int>("GameStartYear"), Completed = false};
                var seasonId = _seasonQuery.Add(initialSeason);

                newGame.CurrentSeasonId = seasonId;
                _gameQuery.Update(newGame);

                _teamQuery.CreateAllTeamsForGame(gameId);
                var teamList = _teamQuery.GetByGame(gameId);

                _teamSeasonQuery.CreateTeamSeasons(teamList, seasonId, gameId);
                _playerCreator.CreateAllPlayersForGame(teamList);
            }

            return gameId;
        }

        // Update new game with selected team
        // PUT api/StartNewGame
        [HttpPut]
        public void Put([FromBody] GameDetails gameDetails)
        {
            _gameQuery.Update(gameDetails);
        }

    }
}
