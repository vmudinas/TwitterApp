using Tweetinvi.Models.V2;
using TwitterApp.Data.Models;

namespace TwitterApp.DataService.Services.Abstractions
{
    public interface IDataService
    {
        int GetTweetCount();

        List<HashTag> GetTopHashTags(int topHashtagNumber);

        void AddUpdateTweet(Tweet twitterData);

        void AddUpdateHashTag(HashtagV2[] hashTagArray, long tweetId);
    }

}
