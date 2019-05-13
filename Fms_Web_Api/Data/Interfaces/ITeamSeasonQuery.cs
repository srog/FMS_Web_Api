using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface ITeamSeasonQuery
    {
        IEnumerable<TeamSeason> GetByGame(int gameDetailsId);
        IEnumerable<TeamSeason> GetByGameAndDivision(int gameDetailsId, int divisionId);
        IEnumerable<TeamSeason> GetByGameSeasonAndDivision(int gameDetailsId, int divisionId, int seasonId);
        IEnumerable<TeamSeason> GetBySeasonAndDivision(int divisionId, int seasonId);
        IEnumerable<TeamSeason> GetByGameAndSeason(int gameDetailsId, int seasonId);
        TeamSeason GetCurrentForTeam(int teamId);
        TeamSeason Get(int id);

        int Add(TeamSeason teamSeason);
        int Update(TeamSeason teamSeason);
       
        int Recalculate(int id);
        void RecalculateDivisionPositions(int seasonId, int divisionId);

    }
}