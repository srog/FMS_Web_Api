using System.Collections.Generic;
using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Queries
{
    public class PlayerStatsQuery : Query, IPlayerStatsQuery
    {
        private const string GET = "spGetPlayerStats";
        private const string INSERT = "spInsertPlayerStats";
        private const string UPDATE = "spUpdatePlayerStats";

        public PlayerStats Get(int playerId)
        {
            return GetSingleById<PlayerStats>(GET, "id", playerId);
        }

        public int Add(PlayerStats playerStats)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    {"playerId", playerStats.PlayerId}
                });
        }
        public int Update(PlayerStats playerStats)
        {
            return Update(UPDATE, new { playerStats.Id, playerStats.Assists, playerStats.CleanSheets, playerStats.Games, playerStats.Goals, playerStats.PlayerId, playerStats.RedCards, playerStats.YellowCards });
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }


}