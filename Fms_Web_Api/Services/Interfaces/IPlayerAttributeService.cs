using System.Collections.Generic;
using Fms_Web_Api.Models;

namespace Fms_Web_Api.Services.Interfaces
{
    public interface IPlayerAttributeService
    {
        List<PlayerAttribute> GetByPlayer(int playerId);
        PlayerAttribute Get(int id);
        int Add(PlayerAttribute playerAttribute);
        int Update(PlayerAttribute playerAttribute);
    }
}
