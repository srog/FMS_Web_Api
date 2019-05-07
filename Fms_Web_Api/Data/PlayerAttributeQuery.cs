using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data
{
    public class PlayerAttributeQuery : Query
    {
        private const string GET = "spGetPlayerAttribute";
        private const string GET_FOR_PLAYER = "spGetPlayerAttributes";
        private const string INSERT = "spInsertPlayerAttribute";
        private const string UPDATE = "spUpdatePlayerAttribute";

        public IEnumerable<PlayerAttribute> GetByPlayer(int playerId)
        {
            return GetAllById<PlayerAttribute>(GET_FOR_PLAYER, "playerId", playerId);
        }

        public PlayerAttribute Get(int id)
        {
            return GetSingle<PlayerAttribute>(GET, id);
        }

        public int Add(PlayerAttribute playerAttribute)
        {
            return Add(INSERT, new Dictionary<string, object>
                {
                    {"playerId", playerAttribute.PlayerId },
                    {"attributeId", playerAttribute.AttributeId },
                    {"attributeValue", playerAttribute.AttributeValue }

                });

        }
        public int Update(PlayerAttribute playerAttribute)
        {
            return Update(UPDATE, new { playerAttribute.Id, playerAttribute.AttributeValue });
        }

    }


}