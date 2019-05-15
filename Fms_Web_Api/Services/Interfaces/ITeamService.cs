using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface ITeamService
    {
        Team Get(int id);
        int Add(Team team);
        int Update(Team team);
        void CreateAllTeamsForGame(int gameId);
        List<Team> GetTeamsForGame(int gameId);
        string GetTeamName(int teamId);
    }
}