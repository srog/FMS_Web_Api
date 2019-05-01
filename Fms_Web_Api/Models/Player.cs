using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class Player
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int GameDetailsId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public int? Position { get; set; }
        [DataMember]
        public int TeamId { get; set; }
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public bool Retired { get; set; }
        [DataMember]
        public int InjuredWeeks { get; set; }
    }
}
