using BaeWatch.GenderAPI._3rdParty;
using BaeWatch.GenderAPI.Data;
using BaeWatch.GenderAPI.Utility;
using System.Collections.Generic;

namespace BaeWatch.GenderAPI
{
    public class GenderAPIEngine
    {
        private TwitterAdapter _twitterAdapter;
        private DatumboxAPI _datumBoxAPI;
        private GenderizeAPI _genderizeAPI;

        public GenderAPIEngine()
        {
            _twitterAdapter = new TwitterAdapter();
            _datumBoxAPI = new DatumboxAPI();
            _genderizeAPI = new GenderizeAPI();
        }

        public GenderResult TwitterGender(string inName_)
        {
            string userTweets = _twitterAdapter.UserTweetsText(inName_);
            string[] profileInfo = _twitterAdapter.UserProfileInfoSimple(inName_);

            string tweetsGenderRes = JSONNetSerializer.JValResult(_datumBoxAPI.GenderDetection(userTweets)).output.result.Value;
            KeyValuePair<string, double> resFromName = _genderizeAPI.GetGender(profileInfo[TwitterAdapter.IDX_NAM]);

            ResultProbabilityPair fromText = new ResultProbabilityPair()
            {
                Description = "Gender Determined based off of user Tweets",
                Result = GetResultFromString(tweetsGenderRes),
                Probability = 1
            };

            ResultProbabilityPair fromName = new ResultProbabilityPair()
            {
                Description = "Gender Determined based off of user's name: " + profileInfo[TwitterAdapter.IDX_NAM],
                Result = GetResultFromString(resFromName.Key),
                Probability = resFromName.Value
            };

            GenderResult rtnResult = new GenderResult()
            {
                UserName = inName_,
                Factors = new ResultProbabilityPair[] { fromText, fromName },
                FinalResult = FinalResult(fromText, fromName)
            };

            return rtnResult;
        }

        public Gender GetResultFromString(string str_)
        {
            if (str_.Equals("male", System.StringComparison.OrdinalIgnoreCase))
                return Gender.Male;
            else if (str_.Equals("female", System.StringComparison.OrdinalIgnoreCase))
                return Gender.Female;
            else
                return Gender.Null;
        }

        public Gender FinalResult(ResultProbabilityPair fromText_, ResultProbabilityPair fromName_)
        {
            double fromTextFactor = 75;
            double fromNameFactor = 25;

            double genderTotal = 0;

            if (fromText_.Result == Gender.Male)
                genderTotal += fromTextFactor * fromText_.Probability ?? 0;

            if (fromText_.Result == Gender.Female)
                genderTotal -= fromTextFactor * fromText_.Probability ?? 0;

            if (fromName_.Result == Gender.Male)
                genderTotal += fromNameFactor * fromName_.Probability ?? 0;

            if (fromName_.Result == Gender.Female)
                genderTotal -= fromNameFactor * fromName_.Probability ?? 0;

            if (genderTotal > 0)
                return Gender.Male;
            else if (genderTotal < 0)
                return Gender.Female;
            else
                return Gender.Null;
        }
    }
}