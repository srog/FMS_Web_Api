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
            return Add(INSERT, new { playerAttributes.Aggression, playerAttributes.Crossing, playerAttributes.Defending, playerAttributes.Form, playerAttributes.Handling, playerAttributes.Happiness, playerAttributes.Morale, playerAttributes.Passing, playerAttributes.PlayerId, playerAttributes.Shooting, playerAttributes.Skills, playerAttributes.Stamina, playerAttributes.Strength, playerAttributes.Tackling });
        }
        public int Update(PlayerAttributes playerAttributes)
        {
            return Update(UPDATE, new { playerAttributes.Id, playerAttributes.Aggression, playerAttributes.Crossing, playerAttributes.Defending, playerAttributes.Form, playerAttributes.Handling, playerAttributes.Happiness, playerAttributes.Morale, playerAttributes.Passing, playerAttributes.PlayerId, playerAttributes.Shooting, playerAttributes.Skills, playerAttributes.Stamina, playerAttributes.Strength, playerAttributes.Tackling });
        }

    }


}