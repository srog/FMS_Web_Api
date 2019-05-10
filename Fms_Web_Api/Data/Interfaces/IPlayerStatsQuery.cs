using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface IPlayerStatsQuery
    {
        PlayerStats Get(int playerId);
        int Add(PlayerStats playerStats);
        int Update(PlayerStats playerStats);
        void Delete(int id);
    }
}