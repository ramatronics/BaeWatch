using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BaeWatch.GenderAPI.Utility
{
    public class JSONNetSerializer
    {
        public static T Deserialize<T>(string inObj_)
        {
            return JsonConvert.DeserializeObject<T>(inObj_);
        }

        public static string Serialize<T>(T inObj_)
        {
            return JsonConvert.SerializeObject(inObj_);
        }

        public static dynamic JValResult(string inObj_)
        {
            return JValue.Parse(inObj_);
        }
    }
}