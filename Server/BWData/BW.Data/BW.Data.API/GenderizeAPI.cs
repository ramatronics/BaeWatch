using BW.Data.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace BW.Data.API
{
    public class GenderizeAPI
    {
        private JavaScriptSerializer _serializer;

        public GenderizeAPI()
        {
            _serializer = new JavaScriptSerializer();
        }

        public string GetGender(string inName_)
        {
            string jsonRtn = CallGenderAPI(Cfg_API.GENDERIZE_URL + inName_);

            Dictionary<string, object> rtnDict = _serializer.Deserialize<Dictionary<string, object>>(jsonRtn);

            return rtnDict["gender"].ToString();
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