using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Enums
{
    [DataContract]
    [Serializable]
    public enum DivisionEnum
    {
        [DataMember]
        PremierLeague = 1,
        [DataMember]
        Championship = 2,
        [DataMember]
        LeagueOne = 3,
        [DataMember]
        LeagueTwo = 4
    }
}
