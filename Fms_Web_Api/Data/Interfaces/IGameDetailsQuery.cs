using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface IGameDetailsQuery
    {
        IEnumerable<GameDetails> GetAll();
        GameDetails Get(int id);
        int Insert(GameDetails gameDetails);
        int Update(GameDetails gameDetails);
        int Delete(int id);
    }
}