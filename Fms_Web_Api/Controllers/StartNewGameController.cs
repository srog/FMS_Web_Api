using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartNewGameController : ControllerBase
    {
        private ITeamService _teamService { get;  }
        private IGameDetailsService _gameDetailsService { get;  }
        private IPlayerCreatorService _playerCreatorService { get; }
        private ISeasonService _seasonService { get; }
        private ITeamSeasonService _teamSeasonService { get; }
        
        public StartNewGameController(
           
            ITeamService teamService,
            IGameDetailsService gameDetailsService,
            IPlayerCreatorService playerCreatorService,
            ISeasonService seasonService,
            ITeamSeasonService teamSeasonService)
        {
            
            _teamSeasonService = teamSeasonService;
            _teamService = teamService;
            _gameDetailsService = gameDetailsService;
            _playerCreatorService = playerCreatorService;
            _seasonService = seasonService;
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

            var gameId = _gameDetailsService.Insert(newGame);
            if (gameId > 0)
            {
                newGame.Id = gameId;

                var seasonId = _seasonService.AddNew(newGame.Id);

                newGame.CurrentSeasonId = seasonId;
                _gameDetailsService.Update(newGame);

                _teamService.CreateAllTeamsForGame(gameId);
                var teamList = _teamService.GetTeamsForGame(gameId);

                _teamSeasonService.CreateForNewGame(teamList, seasonId, gameId);
                _playerCreatorService.CreateAllPlayersForGame(teamList);
            }

            return gameId;
        }

        // Update new game with selected team
        // PUT api/StartNewGame
        [HttpPut]
        public void Put([FromBody] GameDetails gameDetails)
        {
            _gameDetailsService.Update(gameDetails);
        }

    }
}
