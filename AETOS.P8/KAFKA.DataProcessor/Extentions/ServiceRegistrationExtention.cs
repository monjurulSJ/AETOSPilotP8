using Kafka.Library;
using KAFKA.DataProcessor.Connectors;
using KAFKA.DataProcessor.Pipeline;
using KAFKA.DataProcessor.Services;
using Microsoft.Extensions.DependencyInjection;
using P8.Repository.Repositories;

namespace KAFKA.DataProcessor.Extentions
{
    public static class ServiceRegistrationExtention
    {
        public static void AddServiceCollections(this IServiceCollection services)
        {
            services.AddTransient<DataPipeline>()
                    .AddTransient<VehicleService>()
                    .AddTransient<TemperatureService>()
                    .AddTransient<ISource, KAFKASource>()
                    .AddTransient<KafkaConsumer>()
                    .AddTransient<ITopicFactory, TopicFactory>()
                    .AddTransient<IExtractService, ExtractPayload>()
                    .AddTransient<ITemperatureRepository, TemperatureRepository>()
                    //.AddTransient<IVehicleRepository, VehicleRepository>()
                    ;
        }
    }
}
