using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IPlayerService
    {
        List<Player> GetAllPlayersInGame(int gameDetailsId);
        List<Player> GetTeamSquad(int teamId);
        Player Get(int id);
        int Add(Player player);
        int Update(Player player);
        int Retire(int id);
        int AdvanceAllAges(int gameDetailsId);
        void Delete(int gameDetailsId);
    }
}
