using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface IPlayerQuery
    {
        IEnumerable<Player> GetAll(Player player);
        //IEnumerable<Player> GetAllByTeam(int teamId);
        //IEnumerable<Player> GetByGame(int gameDetailsId);
        //IEnumerable<Player> GetAllFreeAgents(int gamedetailsId);
        //IEnumerable<Player> GetAllFreeTransfers(int gamedetailsId);
        Player Get(int id);
        int Add(Player player);
        int Update(Player player);
        int Retire(int id);
        int AdvanceAllAges(int gameDetailsId);
        void Delete(int gameDetailsId);
    }
}