using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface ITeamSeasonService
    {
        List<TeamSeason> GetByGame(int gameDetailsId);
        List<TeamSeason> GetByGameAndDivision(int gameDetailsId, int divisionId);
        List<TeamSeason> GetByGameSeasonAndDivision(int gameDetailsId, int divisionId, int seasonId);
        List<TeamSeason> GetBySeasonAndDivision(int divisionId, int seasonId);
        List<TeamSeason> GetByGameAndSeason(int gameDetailsId, int seasonId);
        TeamSeason GetCurrentForTeam(int teamId);
        TeamSeason Get(int id);

        int Add(TeamSeason teamSeason);
        void CreateForNewGame(IEnumerable<Team> teamList, int seasonId, int gameDetailsId);
        int CreateForNewSeason(int gameDetailsId, int oldSeasonId, int newSeasonId);
        int Recalculate(int id);
        void RecalculateDivisionPositions(int seasonId, int divisionId);
        void RecalculateAll(int seasonId, int divisionId);
    }
}
