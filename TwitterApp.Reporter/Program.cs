using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitterApp.Data;
using TwitterApp.Reporter.Services;
using TwitterApp.Reporter.Services.Interfaces;

namespace TwitterApp.Reporter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host?.Services?.GetService<IReportService>()?.ReadMessages();
            Console.Read();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                    //add your service registrations                    
                    services.AddSingleton<IReportService, ReportService>();
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(databaseName: "TwitterData");
                    }
                    );
                });

            return hostBuilder;
        }
    }
}