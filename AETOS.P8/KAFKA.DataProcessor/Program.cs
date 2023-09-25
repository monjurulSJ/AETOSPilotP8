using KAFKA.DataProcessor.Extentions;
using KAFKA.DataProcessor.HostedService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = CreateHostBuilder(args).Build();
host.Run();

IHostBuilder CreateHostBuilder(string[] strings)
{
    return Host.CreateDefaultBuilder()
         .ConfigureAppConfiguration(app =>
         {
             app.AddJsonFile("appsettings.json", false, true);
         })
        .ConfigureServices((_, services) =>
        {
            services.AddHostedService<StreamProcessor>();
            services.AddServiceCollections();
        });
}
