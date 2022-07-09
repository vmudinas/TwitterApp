using Microsoft.EntityFrameworkCore;
using Moq;
using Tweetinvi.Models.V2;
using TwitterApp.Data;
using TwitterApp.Data.Models;
using TwitterApp.DataService.Services;

namespace TwitterAppTests.Services
{
    public class DataServiceTests
    {
        private MockRepository mockRepository;
        private ApplicationDbContext _dbContext;
        private DataService _dataService;


        public DataServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private void CreateService(string dbName)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(dbName)
              .Options;
            _dbContext = new ApplicationDbContext(options);

            // Arrange
            _dbContext.Tweets.AddRange(
              new Tweet() { Id = 10, Text = "testA" },
              new Tweet() { Id = 12, Text = "testB" },
              new Tweet() { Id = 13, Text = "testC" }
              );

            _dbContext.HashTags.AddRange(
             new HashTag() { Id = Guid.NewGuid(), Count = 5, Text = "TopTag" },
             new HashTag() { Id = Guid.NewGuid(), Count = 2, Text = "LastTag" },
             new HashTag() { Id = Guid.NewGuid(), Count = 4, Text = "MidTag" }
             );

            _dbContext.SaveChanges();

            _dataService = new DataService(_dbContext);

        }

        [Fact]
        public void AddUpdateTweet_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            CreateService("dbAddUpdateTweet");
            Tweet twitterData = new Tweet { Id = 1, AuthorId = "1", Text = "testText" };

            // Act
            _dataService.AddUpdateTweet(
                twitterData);

            // Assert
            Assert.True(_dbContext.Tweets.Any(x=>x.Id == 1));
            Assert.True(_dbContext.Tweets.Any(x => x.Text == "testText"));
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void AddUpdateHashTag_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            CreateService("dbAddUpdateTag");
            var newHashTag = new HashtagV2 { Tag = "tagTest" };
            HashtagV2[] hashTagArray = new List<HashtagV2>() { newHashTag }.ToArray();
            long tweetId = 1;

            // Act
            _dataService.AddUpdateHashTag(
                hashTagArray,
                tweetId);

            // Assert
            Assert.True(_dbContext.HashTags.Any(x => x.Text == "tagTest"));
            Assert.True(_dbContext.HashTags.Any(x => x.TweetId == 1));
            Assert.True(_dbContext.HashTags.Any(x => x.Count == 1));
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void AddUpdateHashTag_StateUnderTest_ExpectedBehaviorMultiHashtags()
        {
            // Arrange
            CreateService("dbAddUpdateTagMulti");
            var newHashTag = new HashtagV2 { Tag = "tagTest" };
            var newHashTag2 = new HashtagV2 { Tag = "AAA" };
            var newHashTag3 = new HashtagV2 { Tag = "tagTest" };

            HashtagV2[] hashTagArray = new List<HashtagV2>() { newHashTag, newHashTag2, newHashTag3 }.ToArray();
            long tweetId = 1;

            // Act
            _dataService.AddUpdateHashTag(
                hashTagArray,
                tweetId);

            // Assert
            Assert.True(_dbContext.HashTags.Any(x => x.Text == "tagTest"));
            Assert.True(_dbContext.HashTags.Any(x => x.Text == "AAA"));
            Assert.True(_dbContext.HashTags.Any(x => x.TweetId == 1));
            Assert.True(_dbContext.HashTags.Any(x => x.Text == "tagTest" && x.Count == 2));
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetTweetCount_StateUnderTest_ExpectedBehavior()
        {
            //Arrange
            CreateService("dbGetTweetCount");
            // Act
            var result = _dataService.GetTweetCount();

            // Assert
            Assert.Equal(3, result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetTopHashTags_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            CreateService("dbTopTag");
            int topHashtagNumber = 3;

            // Act
            var result = _dataService.GetTopHashTags(
                topHashtagNumber);
            
            // Assert
            Assert.True(result.Count == 3);
            this.mockRepository.VerifyAll();
        }
    }
}
