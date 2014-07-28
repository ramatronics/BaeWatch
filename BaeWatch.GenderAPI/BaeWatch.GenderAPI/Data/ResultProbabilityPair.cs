using System.Runtime.Serialization;

namespace BaeWatch.GenderAPI.Data
{
    [DataContract]
    public class ResultProbabilityPair
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Gender Result { get; set; }

        [DataMember]
        public double? Probability { get; set; }
    }
}