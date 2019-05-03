using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class TeamSeason
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int TeamId { get; set; }
        [DataMember]
        public int GameDetailsId { get; set; }
        [DataMember]
        public int SeasonId { get; set; }
        [DataMember]
        public int DivisionId { get; set; }
        [DataMember]
        public int Played { get; set; }
        [DataMember]
        public int Won { get; set; }
        [DataMember]
        public int Drawn { get; set; }
        [DataMember]
        public int Lost { get; set; }
        [DataMember]
        public int Points { get; set; }
        [DataMember]
        public int GoalsFor { get; set; }
        [DataMember]
        public int GoalsAgainst { get; set; }
        [DataMember]
        public int Position { get; set; }

    }
}
