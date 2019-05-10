using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Data.Interfaces
{
    public interface IPlayerAttributeQuery
    {
        IEnumerable<PlayerAttribute> GetByPlayer(int playerId);
        PlayerAttribute Get(int id);
        int Add(PlayerAttribute playerAttribute);
        int Update(PlayerAttribute playerAttribute);
        void Delete(int id);
    }
}