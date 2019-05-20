using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IPlayerService
    {
        List<Player> GetAllPlayersInGame(int gameDetailsId);
        List<Player> GetTeamSquad(int teamId);
        List<Player> GetSelectedTeam(int teamId);
        int GetRandomPlayerFromTeam(int teamId, bool includeKeeper = true, bool includeInjured = true, bool includeSuspended = true);
        Player Get(int id);
        string GetPlayerName(int playerId);
        int Add(Player player);
        int Update(Player player);
        void RecalculateRatingAndValue(int playerId);
        void SetTeamSelection(Team team);
        int Retire(int id);
        int AdvanceSeason(int gameDetailsId);
        int AdvanceWeek(GameDetails gameDetails);
        void Delete(int gameDetailsId);
    }
}
