using BW.Data.Core.Configuration;
using LinqToTwitter;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BW.Data.Twitter
{
    public class TwitterAdapter
    {
        private TwitterContext _twitterCtx;

        public TwitterAdapter(IAuthorizer inAuth_)
        {
            this._twitterCtx = new TwitterContext(inAuth_);
        }

        public string UserText(string inUserName_)
        {
            List<Status> statusList = new List<Status>(from tweet in _twitterCtx.Status
                                                       where tweet.Type == StatusType.User &&
                                                               tweet.ScreenName == inUserName_ &&
                                                               tweet.Count == Cfg_Twitter.AMT_TWEET_QUERY &&
                                                               tweet.RetweetedStatus.StatusID == 0
                                                       select tweet);

            ulong maxID = statusList[statusList.Count - 1].StatusID;
            int prevCount = statusList.Count;

            while (prevCount > 1)
            {
                if (statusList.Count > Cfg_Twitter.AMT_MAX_TWEETS)
                    break;

                List<Status> tmp = (from tweet in _twitterCtx.Status
                                    where tweet.Type == StatusType.User &&
                                            tweet.ScreenName == inUserName_ &&
                                            tweet.Count == Cfg_Twitter.AMT_TWEET_QUERY &&
                                            tweet.RetweetedStatus.StatusID == 0 &&
                                            tweet.MaxID == maxID
                                    select tweet).ToList();
                if (tmp.Count == 1)
                    break;

                statusList.AddRange(tmp);
                maxID = tmp[tmp.Count - 1].StatusID;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < statusList.Count; i++)
                sb.Append(statusList[i].Text + " ");

            return sb.ToString();
        }

        public string[] UserProfileInfo(string inUserIdentifier_)
        {
            User tmp =
                    (from tweet in _twitterCtx.User
                     where tweet.Type == UserType.Show &&
                           tweet.ScreenName == inUserIdentifier_
                     select tweet).SingleOrDefault();

            return new string[] { tmp.ProfileImage, tmp.Location, tmp.Description, tmp.Name };
        }

        public ulong[] UserFollowers(string inUserIdentifier_)
        {
            Friendship tmp =
                   (from fship in _twitterCtx.Friendship
                    where fship.Type == FriendshipType.FollowerIDs &&
                          fship.ScreenName == inUserIdentifier_
                    select fship).SingleOrDefault();

            return tmp.IDInfo.IDs.ToArray();
        }

        public ulong[] UserFollowing(string inUserIdentifier_)
        {
            Friendship tmp =
                    (from fship in _twitterCtx.Friendship
                     where fship.Type == FriendshipType.FriendIDs &&
                           fship.ScreenName == inUserIdentifier_
                     select fship).SingleOrDefault();

            return tmp.IDInfo.IDs.ToArray();
        }
    }
}