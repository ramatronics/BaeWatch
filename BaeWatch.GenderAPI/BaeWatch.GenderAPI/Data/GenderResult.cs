using System.Runtime.Serialization;

namespace BaeWatch.GenderAPI.Data
{
    public enum Gender { Male, Female, Null };

    [DataContract]
    public class GenderResult
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public ResultProbabilityPair[] Factors { get; set; }

        [DataMember]
        public Gender FinalResult { get; set; }
    }
}