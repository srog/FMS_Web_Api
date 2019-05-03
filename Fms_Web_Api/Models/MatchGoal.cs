using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class MatchGoal
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int MatchId { get; set; }
        [DataMember]
        public int Minute { get; set; }
        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public int TeamId { get; set; }
        [DataMember]
        public int AssistPlayerId { get; set; }
        [DataMember]
        public bool OwnGoal { get; set; }
    }
}
