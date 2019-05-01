using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class PlayerStats
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public int Games { get; set; }
        [DataMember]
        public int Goals { get; set; }
        [DataMember]
        public int Assists { get; set; }
        [DataMember]
        public int CleanSheets { get; set; }

        public PlayerStats(int playerId)
        {
            PlayerId = playerId;
            Games = 0;
            Goals = 0;
            Assists = 0;
            CleanSheets = 0;
        }
    }
}
