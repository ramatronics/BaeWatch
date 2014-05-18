using System;
using System.Runtime.Serialization;

namespace BW.Data.Lib
{
    [DataContract]
    public class Tweet
    {
        [DataMember]
        public ulong StatusId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime Created { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public ulong[] AssociatedMembers { get; set; }
    }
}