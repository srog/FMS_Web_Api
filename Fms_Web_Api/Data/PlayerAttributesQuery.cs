using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class PlayerAttributesQuery : Query
    {
        private const string GET = "spGetPlayerAttributes";
        private const string INSERT = "spInsertPlayerAttributes";
        private const string UPDATE = "spUpdatePlayerAttributes";

        public PlayerAttributes Get(int playerId)
        {
            return GetSingleById<PlayerAttributes>(GET, "playerId", playerId);
        }

        

        public int Add(PlayerAttributes playerAttributes)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    {"playerId", playerAttributes.PlayerId },
                    {"aggression", playerAttributes.Aggression },
                    {"crossing", playerAttributes.Crossing },
                    {"defending", playerAttributes.Defending },
                    {"form", playerAttributes.Form },
                    {"handling", playerAttributes.Handling },
                    {"happiness", playerAttributes.Happiness },
                    {"morale", playerAttributes.Morale },
                    {"passing", playerAttributes.Passing },
                    {"shooting", playerAttributes.Shooting },
                    {"skills", playerAttributes.Skills },
                    {"speed", playerAttributes.Speed },
                    {"stamina", playerAttributes.Stamina },
                    {"strength", playerAttributes.Strength },
                    {"tackling", playerAttributes.Tackling }
                });

        }
        public int Update(PlayerAttributes playerAttributes)
        {
            return Update(UPDATE, new { playerAttributes.Id, playerAttributes.Aggression, playerAttributes.Crossing, playerAttributes.Defending, playerAttributes.Form, playerAttributes.Handling, playerAttributes.Happiness, playerAttributes.Morale, playerAttributes.Passing, playerAttributes.PlayerId, playerAttributes.Shooting, playerAttributes.Skills, playerAttributes.Stamina, playerAttributes.Strength, playerAttributes.Tackling });
        }

    }


}