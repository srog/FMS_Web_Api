using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class Team
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int YearFormed { get; set; }
        [DataMember]
        public int Division { get; set; }
        [DataMember]
        public int Cash { get; set; }
        [DataMember]
        public int StadiumCapacity { get; set; }
        [DataMember]
        public int GameDetailsId { get; set; }
    }
}
