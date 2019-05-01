using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class PlayerQuery : Query
    {
        private const string GET_ALL = "spGetAllPlayers";
        private const string GET = "spGetPlayerById";
        private const string INSERT = "spInsertPlayer";
        private const string UPDATE = "spUpdatePlayer";

        public void CreateAll()
        {

        }
        public IEnumerable<Player> GetAll()
        {
            return GetAll<Player>(GET_ALL);
        }
        public Player Get(int id)
        {
            return GetSingle<Player>(GET, id);
        }
        public int Add(Player player)
        {
            return Add<Player>(INSERT, new { player.Age, player.InjuredWeeks, player.Position, player.Name, player.Rating, player.Retired, player.TeamId, player.Value });
        }
        public int Update(Player player)
        {
            return Update<Player>(UPDATE, new { player.Id, player.Age, player.InjuredWeeks, player.Position, player.Name, player.Rating, player.Retired, player.TeamId, player.Value });
        }
        public int Delete(int id)
        {
            return Delete(id);
        }

    }


}