using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BW.Data.Lib
{
    [DataContract]
    public class User
    {
        [DataMember]
        public ulong Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public ulong CrushedOnBy { get; set; }

        [DataMember]
        public ulong Crushes { get; set; }

        [DataMember]
        public Settings Settings { get; set; }

        [DataMember]
        public Gender Sex { get; set; }
    }

    [DataContract]
    public enum Gender
    {
        [EnumMember]
        Male,

        [EnumMember]
        Female,
        
        [EnumMember]
        Unknown,

        [EnumMember]
        Other
    }
}
