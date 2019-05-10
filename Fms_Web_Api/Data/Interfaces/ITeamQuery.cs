using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface ITeamQuery
    {
        void CreateAllTeamsForGame(int gameId);
        IEnumerable<Team> GetAll();
        //IEnumerable<Team> GetByDivision(int divisionId);
        IEnumerable<Team> GetByGame(int gameDetailsId);
        Team Get(int id);
        int Add(Team team);
        int Update(Team team);
        int Delete(int gameDetailsId);

    }
}