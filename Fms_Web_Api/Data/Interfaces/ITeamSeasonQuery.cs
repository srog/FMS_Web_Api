using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface ITeamSeasonQuery
    {
        IEnumerable<TeamSeason> GetByGame(int gameDetailsId);
        IEnumerable<TeamSeason> GetByGameAndDivision(int gameDetailsId, int divisionId);
        IEnumerable<TeamSeason> GetByGameSeasonAndDivision(int gameDetailsId, int divisionId, int seasonId);
        IEnumerable<TeamSeason> GetByGameAndSeason(int gameDetailsId, int seasonId);
        TeamSeason GetCurrentForTeam(int teamId);
        TeamSeason Get(int id);

        int Add(TeamSeason teamSeason);
        void CreateForNewGame(IEnumerable<Team> teamList, int seasonId, int gameDetailsId);
        int CreateForNewSeason(int gameDetailsId, int oldSeasonId, int newSeasonId);
        int Recalculate(int id);
        void RecalculateDivisionPositions(int seasonId, int divisionId);
        

    }
}