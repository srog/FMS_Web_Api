using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IGameDetailsService
    {
        List<GameDetails> GetAll();
        GameDetails Get(int id);
        int Insert(GameDetails gameDetails);
        int Update(GameDetails gameDetails);
        int AdvanceWeek(GameDetails gameDetails);
        int AdvanceSeason(GameDetails gameDetails);
        int Delete(int id);
    }
}
