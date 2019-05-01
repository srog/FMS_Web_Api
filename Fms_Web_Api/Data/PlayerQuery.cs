using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class PlayerQuery : Query
    {
        private const string GET_ALL = "spGetAllPlayers";
        private const string GET_ALL_FREE_AGENTS = "spGetAllFreeAgents";
        private const string GET_ALL_FREE_TRANSFERS = "spGetAllFreeTransfers";
        private const string GET_ALL_FOR_TEAM = "spGetPlayersByTeam";
        private const string GET = "spGetPlayerById";
        private const string INSERT = "spInsertPlayer";
        private const string UPDATE = "spUpdatePlayer";
        private const string DELETE = "spDeletePlayer";

        // experimental
        private const string RETIRE_PLAYER = "spRetirePlayer";
        private const string ADVANCE_ALL_PLAYER_AGES = "spAdvanceAllPlayerAges";

        public void CreateAll()
        {

        }
        public IEnumerable<Player> GetAll()
        {
            return GetAll<Player>(GET_ALL);
        }
        public IEnumerable<Player> GetAllByTeam(int teamId)
        {
            return GetAllById<Player>(GET_ALL_FOR_TEAM, "teamId", teamId);
        }

        public IEnumerable<Player> GetAllFreeAgents(int gamedetailsId)
        {
            return GetAllById<Player>(GET_ALL_FREE_AGENTS, "gameDetailsId", gamedetailsId);
        }

        public IEnumerable<Player> GetAllFreeTransfers(int gamedetailsId)
        {
            return GetAllById<Player>(GET_ALL_FREE_TRANSFERS, "gameDetailsId", gamedetailsId);
        }


        public Player Get(int id)
        {
            return GetSingle<Player>(GET, id);
        }
        public int Add(Player player)
        {
            return Add(INSERT, new { player.GameDetailsId, player.Age, player.InjuredWeeks, player.Position, player.Name, player.Rating, player.Retired, player.TeamId, player.Value });
        }
        public int Update(Player player)
        {
            return Update(UPDATE, new { player.Id, player.Age, player.InjuredWeeks, player.Position, player.Name, player.Rating, player.Retired, player.TeamId, player.Value });
        }
  
        public int Retire(int id)
        {
            return UpdateSingleColumn(RETIRE_PLAYER, "id", id);
        }

        public int AdvanceAllAges(int gameDetailsId)
        {
            return UpdateSingleColumn(ADVANCE_ALL_PLAYER_AGES, "gameDetailsId", gameDetailsId);
        }
    }


}