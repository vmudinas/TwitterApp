using Microsoft.Extensions.Hosting;
using TwitterApp.DataService.Services.Abstractions;

namespace TwitterApp.DataService.Services
{
    public class TwitterService : BackgroundService
    {
        private ITwitterDataService _twitterDataService;
        private IReportService _reportService;

        public TwitterService(ITwitterDataService twitterDataService, IReportService reportService)
        {
            _twitterDataService = twitterDataService;
            _reportService = reportService;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Task.Run(async () => await _twitterDataService.GetTwitterData(), cancellationToken);

                Task.Run(async () => await _reportService.ReadMessages(), cancellationToken);


            }, cancellationToken);
           ;
        }
    }
}
