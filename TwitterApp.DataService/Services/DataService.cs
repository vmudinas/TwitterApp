using Microsoft.EntityFrameworkCore;
using Tweetinvi.Models.V2;
using TwitterApp.Data;
using TwitterApp.Data.Models;
using TwitterApp.DataService.Services.Abstractions;

namespace TwitterApp.DataService.Services
{
    public class DataService : IDataService
    {
        private ApplicationDbContext _context;

        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUpdateTweet(Tweet twitterData)
        {
            lock (_context)
            {
                if (_context.Tweets.Any(x => x.Id == twitterData.Id))
                {
                    _context.Tweets.Update(twitterData);
                }
                else
                {
                    _context.Tweets.AddAsync(twitterData);
                }

                _context.SaveChangesAsync();
            }
        }

        public void AddUpdateHashTag(HashtagV2[] hashTagArray, long tweetId)
        {
            

                foreach (var hashTagV2 in hashTagArray)
                {
                lock (_context)
                {
                    if (hashTagV2.Tag.Length > 0)
                    {
                        var hashTag = _context.HashTags.FirstOrDefault(x => x.Text == hashTagV2.Tag);

                        if (hashTag == null)
                        {
                            var newHashtag = new HashTag { Id = Guid.NewGuid(), Count = 1, Text = hashTagV2.Tag, TweetId = tweetId };
                            _context.HashTags.Add(newHashtag);
                        }
                        else
                        {
                            hashTag.Count++;
                            _context.HashTags.Update(hashTag);
                        }

                    }
                    _context.SaveChangesAsync();
                }
            }
               

        }

        public int GetTweetCount()
        {
            lock (_context)
            {
                return _context.Tweets.AsNoTracking().Count();
            }
        }

        public List<HashTag> GetTopHashTags(int topHashtagNumber)
        {

            lock (_context)
            {
                return _context.HashTags.AsNoTracking().ToList().OrderByDescending(x => x.Count).Take(topHashtagNumber).ToList();
            }
        }
    }
}
