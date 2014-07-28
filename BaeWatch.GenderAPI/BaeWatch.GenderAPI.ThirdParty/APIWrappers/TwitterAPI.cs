using LinqToTwitter;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BaeWatch.GenderAPI._3rdParty
{
    public class TwitterAdapter
    {
        private const int AMT_TWEET_QUERY = 200;
        private const int AMT_MAX_TWEETS = 1000;

        public const int IDX_IMG = 0;
        public const int IDX_LOC = 1;
        public const int IDX_BIO = 2;
        public const int IDX_NAM = 3;

        private TwitterContext _twitterCtx;

        public TwitterAdapter()
        {
            this._twitterCtx = new TwitterContext(GetSingleUserAuth());
        }

        public List<Status> UserTweets(string inUserName_)
        {
            List<Status> statusList = new List<Status>(from tweet in _twitterCtx.Status
                                                       where tweet.Type == StatusType.User &&
                                                               tweet.ScreenName == inUserName_ &&
                                                               tweet.Count == AMT_TWEET_QUERY &&
                                                               tweet.RetweetedStatus.StatusID == 0
                                                       select tweet);

            ulong maxID = statusList[statusList.Count - 1].StatusID;
            int prevCount = statusList.Count;

            while (prevCount > 1)
            {
                if (statusList.Count > AMT_MAX_TWEETS)
                    break;

                List<Status> tmp = (from tweet in _twitterCtx.Status
                                    where tweet.Type == StatusType.User &&
                                            tweet.ScreenName == inUserName_ &&
                                            tweet.Count == AMT_TWEET_QUERY &&
                                            tweet.RetweetedStatus.StatusID == 0 &&
                                            tweet.MaxID == maxID
                                    select tweet).ToList();
                if (tmp.Count == 1)
                    break;

                statusList.AddRange(tmp);
                maxID = tmp[tmp.Count - 1].StatusID;
            }

            return statusList;
        }

        public string UserTweetsText(string inUserName_)
        {
            List<Status> tmpStatusList = UserTweets(inUserName_);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tmpStatusList.Count; i++)
                sb.Append(tmpStatusList[i].Text + "\n");

            return sb.ToString();
        }

        public User UserProfileInfo(string inUserIdentifier_)
        {
            return
                    (from tweet in _twitterCtx.User
                     where tweet.Type == UserType.Show &&
                           tweet.ScreenName == inUserIdentifier_
                     select tweet).SingleOrDefault();
        }

        public string[] UserProfileInfoSimple(string inUserIdentifier_)
        {
            User tmp = this.UserProfileInfo(inUserIdentifier_);
            return new string[] { tmp.ProfileImage, tmp.Location, tmp.Description, tmp.Name };
        }

        public Friendship UserFollowers(string inUserIdentifier_)
        {
            Friendship tmp =
                   (from fship in _twitterCtx.Friendship
                    where fship.Type == FriendshipType.FollowerIDs &&
                          fship.ScreenName == inUserIdentifier_
                    select fship).SingleOrDefault();

            return tmp;
            //return tmp.IDInfo.IDs.ToArray();
        }

        public Friendship UserFollowing(string inUserIdentifier_)
        {
            Friendship tmp =
                    (from fship in _twitterCtx.Friendship
                     where fship.Type == FriendshipType.FriendIDs &&
                           fship.ScreenName == inUserIdentifier_
                     select fship).SingleOrDefault();

            return tmp;
            //return tmp.IDInfo.IDs.ToArray();
        }

        public static IAuthorizer GetSingleUserAuth()
        {
            return new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = ConfigurationManager.AppSettings["consumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"],
                    AccessToken = ConfigurationManager.AppSettings["accessToken"],
                    AccessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]
                }
            };
        }
    }
}