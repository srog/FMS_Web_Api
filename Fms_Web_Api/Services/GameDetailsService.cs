using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class GameDetailsService : IGameDetailsService
    {
        private readonly IGameDetailsQuery _gameDetailsQuery;
        private readonly IPlayerService _playerService;

        public GameDetailsService(IGameDetailsQuery gameDetailsQuery, IPlayerService playerService)
        {
            _gameDetailsQuery = gameDetailsQuery;
            _playerService = playerService;
        }
        #region Implementation of IGameDetailsService

        public List<GameDetails> GetAll()
        {
            return _gameDetailsQuery.GetAll().ToList();
        }

        public GameDetails Get(int id)
        {
            return _gameDetailsQuery.Get(id);
        }

        public int Insert(GameDetails gameDetails)
        {
            return _gameDetailsQuery.Insert(gameDetails);
        }

        public int Update(GameDetails gameDetails)
        {
            return _gameDetailsQuery.Update(gameDetails);
        }


        public int AdvanceWeek(GameDetails gameDetails)
        {
            gameDetails.CurrentWeek++;
            _gameDetailsQuery.Update(gameDetails);
            _playerService.AdvanceWeek(gameDetails);
            return 0;
        }
        public int AdvanceSeason(GameDetails gameDetails)
        {
            // TODO 

            return 0;
        }

        public int Delete(int id)
        {
            return _gameDetailsQuery.Delete(id);
        }

        #endregion
    }
}
