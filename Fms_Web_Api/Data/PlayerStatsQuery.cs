using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class PlayerStatsQuery : Query
    {
        private const string GET = "spGetPlayerStats";
        private const string INSERT = "spInsertPlayerStats";
        private const string UPDATE = "spUpdatePlayerStats";

        public PlayerStats Get(int playerId)
        {
            return GetSingleById<PlayerStats>(GET, "playerId", playerId);
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
    }


}