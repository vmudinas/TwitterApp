using TwitterApp.Client.Abstractions;
using TwitterApp.DataService.Services.Abstractions;

namespace TwitterApp.DataService.Services
{
    public class TwitterDataService : ITwitterDataService
    {
        private ITwitterClient _client;

        public TwitterDataService(ITwitterClient client)
        {          
            _client = client;
        }

        public async Task GetTwitterData()
        {
            await _client.GetSampleStreamAsync(CancellationToken.None);

        }
    }
}
