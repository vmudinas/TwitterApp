using Microsoft.Extensions.Logging;
using System.Text;
using TwitterApp.DataService.Services.Abstractions;

namespace TwitterApp.DataService.Services
{
    public class ReportService : IReportService
    {
        //We inject the DBContext into the controller...
        private IDataService _dataService;
        private readonly ILogger _logger;

        public ReportService(IDataService dataService, ILogger<ReportService> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public Task ReadMessages()
        {
            //Display Recievied data every 20 seconds min 20000 miliseconds
            var timeout = 20000;
            // Display Chinese and other characters
            // This part would require more testing to make sure encoding is correct. 
            // Had some issues with Chinese characters that had to be encoided correctly with UTF8
            // More research required, might be an issue with sdk or IDE

            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            // Get 10 top hashtags
            var topHashtagNumber = 10;
            while (true)
            {              
                Task.Delay(timeout).Wait();
                try
                {
                  
                    Console.WriteLine("########################");
                    Console.WriteLine($"Total number of tweets received: {_dataService.GetTweetCount()}");
                    Console.WriteLine($"Top Hashtags:");

                    var tempTop = 1;
                    foreach (var topHashtag in _dataService.GetTopHashTags(topHashtagNumber))
                    {
                        Console.WriteLine($"{tempTop} hastag: {topHashtag.Text}");
                        tempTop++;
                    }
                    

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
               
            }
        }
    }
}
