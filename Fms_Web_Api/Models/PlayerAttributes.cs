using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class PlayerAttributes
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public int Speed { get; set; }
        [DataMember]
        public int Shooting { get; set; }
        [DataMember]
        public int Crossing { get; set; }
        [DataMember]
        public int Passing { get; set; }
        [DataMember]
        public int Skills { get; set; }
        [DataMember]
        public int Tackling { get; set; }
        [DataMember]
        public int Defending { get; set; }
        [DataMember]
        public int Handling { get; set; }
        [DataMember]
        public int Aggression { get; set; }
        [DataMember]
        public int Form { get; set; }
        [DataMember]
        public int Morale { get; set; }
        [DataMember]
        public int Happiness { get; set; }
        [DataMember]
        public int Stamina { get; set; }
        [DataMember]
        public int Strength { get; set; }


    }
}
