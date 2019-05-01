using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class Season
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int GameDetailsId { get; set; }
        [DataMember]
        public int StartYear { get; set; }
        [DataMember]
        public bool Completed { get; set; }


    }
}
