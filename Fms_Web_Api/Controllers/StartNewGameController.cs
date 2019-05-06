using System.Linq;
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
        private readonly SeasonQuery _seasonQuery = new SeasonQuery();
        private readonly TeamSeasonQuery _teamSeasonQuery = new TeamSeasonQuery();

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

                var initialSeason = new Season {GameDetailsId = newGame.Id, StartYear = 2019, Completed = false};
                var seasonId = _seasonQuery.Add(initialSeason);

                newGame.CurrentSeasonId = seasonId;
                _gameQuery.Update(newGame);

                _teamQuery.CreateAllTeamsForGame(gameId);
                var teamList = _teamQuery.GetByGame(gameId);
                var index = 0;

                foreach (var team in teamList)
                {
                    index++;
                    _teamSeasonQuery.Add(new TeamSeason
                        {
                            DivisionId = team.DivisionId,
                            SeasonId = seasonId,
                            TeamId = team.Id,
                            GameDetailsId = gameId,
                            Position = index
                        });
                }

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
