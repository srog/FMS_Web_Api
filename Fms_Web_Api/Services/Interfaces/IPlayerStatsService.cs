using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IPlayerStatsService
    {
        PlayerStats Get(int playerId);
        int Add(PlayerStats playerStats);
        int Update(PlayerStats playerStats);
    }
}
