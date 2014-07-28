using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace BaeWatch.GenderAPI._3rdParty
{
    public class GenderizeAPI
    {
        public const string GENDERIZE_URL = "http://api.genderize.io/?name=";

        private JavaScriptSerializer _serializer;

        public GenderizeAPI()
        {
            _serializer = new JavaScriptSerializer();
        }

        public KeyValuePair<string, double> GetGender(string inName_)
        {
            string jsonRtn = CallGenderAPI(GENDERIZE_URL + inName_);

            Dictionary<string, string> rtnDict = _serializer.Deserialize<Dictionary<string, string>>(jsonRtn);

            return new KeyValuePair<string, double>(rtnDict["gender"] ?? string.Empty, Convert.ToDouble(rtnDict["probability"]));
        }

        private string CallGenderAPI(string inUrl_)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(inUrl_);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                }
                throw;
            }
        }
    }
}