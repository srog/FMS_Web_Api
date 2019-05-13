using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;

namespace Fms_Web_Api.Services
{
    public class PlayerStatsService : IPlayerStatsService
    {
        private readonly IPlayerStatsQuery _playerStatsQuery;

        public PlayerStatsService(IPlayerStatsQuery playerStatsQuery)
        {
            _playerStatsQuery = playerStatsQuery;
        }
        #region Implementation of IPlayerStatsService

        public PlayerStats Get(int playerId)
        {
            return _playerStatsQuery.Get(playerId);
        }

        public int Add(PlayerStats playerStats)
        {
            return _playerStatsQuery.Add(playerStats);
        }

        public int Update(PlayerStats playerStats)
        {
            return _playerStatsQuery.Update(playerStats);
        }
        #endregion
    }
}
