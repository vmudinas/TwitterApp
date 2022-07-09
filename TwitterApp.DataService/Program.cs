using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TwitterApp.Client;
using TwitterApp.Client.Abstractions;
using TwitterApp.Data;
using TwitterApp.DataService.Services;
using TwitterApp.DataService.Services.Abstractions;

namespace TwitterApp.DataService
{
        public class Program
        {

        public static IConfigurationRoot? Configuration { get; set; }
        static async Task Main(string[] args)
            {
                var host = CreateHostBuilder(args).Build();
                await host.StartAsync();
                Console.WriteLine("Service Started:");
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
                        services.AddScoped<ITwitterDataService, TwitterDataService>();
                        services.AddScoped<IReportService, ReportService>();
                        services.AddHostedService<TwitterService>();
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            object value = options.UseInMemoryDatabase(databaseName: "Tweet");
                        });
                        services.AddLogging(
                           builder =>
                           {
                               builder.AddFilter("Microsoft", LogLevel.Warning)
                                      .AddFilter("System", LogLevel.Warning)
                                      .AddFilter("NToastNotify", LogLevel.Warning)
                                      .AddConsole();
                           });

                        // Build configuration
                        Configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                            .AddJsonFile("appsettings.json", false)
                            .Build();

                        // Add access to generic IConfigurationRoot
                        services.AddSingleton<IConfigurationRoot>(Configuration);


                        // Hard Coded Twitter Api keys and tokens just for this app. 
                        // Should be moved to config. apps.settings.json
                        services.AddSingleton<ITwitterClient>(provider =>
                                new TwitterClient(Configuration["TwitterKeys:apiKey"],
                                Configuration["TwitterKeys:ApiKey"],
                                Configuration["TwitterKeys:BerearToken"],
                                provider.GetService<IDataService>(), provider.GetService<ILogger<TwitterClient>>()
                                )
                            );

                        services.AddSingleton<IDataService, Services.DataService>();
                    });

                return hostBuilder;
            }
        }
}