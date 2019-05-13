using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IPlayerCreatorService
    {
        void CreateAllPlayersForGame(IEnumerable<Team> teamList);
    }
}