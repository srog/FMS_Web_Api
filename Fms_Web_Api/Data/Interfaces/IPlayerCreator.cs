using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface IPlayerCreator
    {
        void CreateAllPlayersForGame(IEnumerable<Team> teamList);
    }
}