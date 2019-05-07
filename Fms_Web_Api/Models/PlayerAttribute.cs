using System;
using System.Runtime.Serialization;

namespace Fms_Web_Api.Models
{
    [DataContract]
    [Serializable]
    public class PlayerAttribute
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public int AttributeId { get; set; }
        [DataMember]
        public int AttributeValue { get; set; }

    }
}
