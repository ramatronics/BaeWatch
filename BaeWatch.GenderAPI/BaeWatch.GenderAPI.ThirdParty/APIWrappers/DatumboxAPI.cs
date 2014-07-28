using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace BaeWatch.GenderAPI._3rdParty
{
    public class DatumboxAPI
    {
        private readonly string _apiKey = System.Configuration.ConfigurationManager.AppSettings["DatumBoxAPIKey"];

        public DatumboxAPI()
        {
        }

        private void AddArgument(Dictionary<string, string> arguments, string key, string value)
        {
            arguments.Remove(key);
            arguments.Add(key, value);
        }

        private string GetArguments(Dictionary<string, string> arguments)
        {
            StringBuilder parameters = new StringBuilder();

            foreach (KeyValuePair<string, string> kvp in arguments)
                EncodeAndAddItem(parameters, kvp.Key, kvp.Value);

            return parameters.ToString();
        }

        private void EncodeAndAddItem(StringBuilder baseRequest, string key, string dataItem)
        {
            if (baseRequest == null)
                baseRequest = new StringBuilder();

            if (baseRequest.Length != 0)
                baseRequest.Append("&");

            baseRequest.Append(key);
            baseRequest.Append("=");
            baseRequest.Append(HttpUtility.UrlEncode(dataItem));
        }

        private string SendPostRequest(string URL, Dictionary<string, string> arguments)
        {
            HttpWebRequest request;
            string postData = GetArguments(arguments);

            Uri uri = new Uri(URL);
            request = (HttpWebRequest)WebRequest.Create(uri);

            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;

            using (Stream writeStream = request.GetRequestStream())
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(postData);
                writeStream.Write(bytes, 0, bytes.Length);
            }

            request.AllowAutoRedirect = true;

            UTF8Encoding enc = new UTF8Encoding();

            string result = string.Empty;

            HttpWebResponse Response;
            try
            {
                using (Response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = Response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            return readStream.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

        public string SentimentAnalysis(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/SentimentAnalysis.json", arguments);
        }

        public string TwitterSentimentAnalysis(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/TwitterSentimentAnalysis.json", arguments);
        }

        public string SubjectivityAnalysis(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/SubjectivityAnalysis.json", arguments);
        }

        public string TopicClassification(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/TopicClassification.json", arguments);
        }

        public string SpamDetection(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/SpamDetection.json", arguments);
        }

        public string AdultContentDetection(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/AdultContentDetection.json", arguments);
        }

        public string ReadabilityAssessment(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/ReadabilityAssessment.json", arguments);
        }

        public string LanguageDetection(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/LanguageDetection.json", arguments);
        }

        public string CommercialDetection(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/CommercialDetection.json", arguments);
        }

        public string EducationalDetection(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/EducationalDetection.json", arguments);
        }

        public string GenderDetection(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/GenderDetection.json", arguments);
        }

        public string TextExtraction(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);

            return SendPostRequest("http://api.datumbox.com/1.0/TextExtraction.json", arguments);
        }

        public string KeywordExtraction(string text, int n)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "text", text);
            AddArgument(arguments, "n", n.ToString());

            return SendPostRequest("http://api.datumbox.com/1.0/KeywordExtraction.json", arguments);
        }

        public string DocumentSimilarity(string original, string copy)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            AddArgument(arguments, "api_key", _apiKey);
            AddArgument(arguments, "original", original);
            AddArgument(arguments, "copy", copy);

            return SendPostRequest("http://api.datumbox.com/1.0/DocumentSimilarity.json", arguments);
        }
    }
}