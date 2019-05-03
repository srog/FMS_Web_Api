using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class Match
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int GameDetailsId { get; set; }
        [DataMember]
        public int SeasonId { get; set; }
        [DataMember]
        public int DivisionId { get; set; }
        [DataMember]
        public int Week { get; set; }
        [DataMember]
        public int HomeTeamId { get; set; }
        [DataMember]
        public int AwayTeamId { get; set; }
        [DataMember]
        public int HomeTeamScore { get; set; }
        [DataMember]
        public int AwayTeamScore { get; set; }
        [DataMember]
        public bool Completed { get; set; }
    }
}
