using System.Collections.Generic;
using System.Linq;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerQuery _playerQuery;

        public PlayerService(IPlayerQuery playerQuery)
        {
            _playerQuery = playerQuery;
        }
        #region Implementation of IPlayerService

        public List<Player> GetAllPlayersInGame(int gameDetailsId)
        {
            return _playerQuery.GetAll(new Player { GameDetailsId = gameDetailsId }).ToList();
        }
        public List<Player> GetTeamSquad(int teamId)
        {
            return _playerQuery.GetAll(new Player { TeamId = teamId}).ToList();
        }

        public Player Get(int id)
        {
            return _playerQuery.Get(id);
        }

        public int Add(Player player)
        {
            return _playerQuery.Add(player);
        }

        public int Update(Player player)
        {
            return _playerQuery.Update(player);
        }

        public int Retire(int id)
        {
            return _playerQuery.RetirePlayer(id);
        }

        public int AdvanceAllAges(int gameDetailsId)
        {
            return _playerQuery.AdvanceAllAges(gameDetailsId);
        }

        public void Delete(int gameDetailsId)
        {
            _playerQuery.Delete(gameDetailsId);
        }

        #endregion
    }
}
