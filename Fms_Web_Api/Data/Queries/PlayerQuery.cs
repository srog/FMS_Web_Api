using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Queries
{
    public class PlayerQuery : Query, IPlayerQuery
    {
        private const string GET_ALL = "spGetAllPlayers";
        private const string GET = "spGetPlayerById";
        private const string INSERT = "spInsertPlayer";
        private const string UPDATE = "spUpdatePlayer";
        private const string DELETE = "spDeletePlayer";

        // experimental
        private const string RETIRE_PLAYER = "spRetirePlayer";
        private const string ADVANCE_ALL_PLAYER_AGES = "spAdvanceAllPlayerAges";

        public IEnumerable<Player> GetAll(Player player)
        {
            var param = new
                {
                    player.GameDetailsId,
                    player.TeamId
                };
            return GetAll<Player>(GET_ALL, param);
        }

        public Player Get(int id)
        {
            return GetSingle<Player>(GET, id);
        }
        public int Add(Player player)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    { "age", player.Age },
                    { "injuredWeeks", player.InjuredWeeks },
                    { "position", player.Position },
                    { "name", player.Name },
                    { "rating", player.Rating },
                    { "retired", player.Retired },
                    { "teamId", player.TeamId },
                    { "value", player.Value },
                    { "gameDetailsId", player.GameDetailsId }
                });

      }
        public int Update(Player player)
        {
            return Update(UPDATE, new
                {
                    player.Id,
                    player.Age,
                    player.InjuredWeeks,
                    player.Position,
                    player.Name,
                    player.Rating,
                    player.Retired,
                    player.TeamId,
                    player.Value
                });
        }
  
        public int RetirePlayer(int id)
        {
            return UpdateSingleColumn(RETIRE_PLAYER, "id", id);
        }

        public int AdvanceAllAges(int gameDetailsId)
        {
            return UpdateSingleColumn(ADVANCE_ALL_PLAYER_AGES, "gameDetailsId", gameDetailsId);
        }

        public void Delete(int gameDetailsId)
        {
            Delete(DELETE, gameDetailsId);
        }
    }


}