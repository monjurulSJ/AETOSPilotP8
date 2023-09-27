using KAFKA.DataProcessor.Extentions;
using KAFKA.DataProcessor.HostedService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P8.Model.DbContexts;
using System.Reflection;

using IHost host = CreateHostBuilder(args).Build();
host.Run();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var assemblyName = Assembly.GetExecutingAssembly().FullName;

         
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString, m => m.MigrationsAssembly(assemblyName));
            });

            services.AddHostedService<StreamProcessor>();
            services.AddServiceCollections();
        });