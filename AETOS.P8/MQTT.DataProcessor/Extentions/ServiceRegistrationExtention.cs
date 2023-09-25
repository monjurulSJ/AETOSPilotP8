using Kafka.Library;
using Microsoft.Extensions.DependencyInjection;
using MQTT.DataProcessor.Connectors;
using MQTT.DataProcessor.Pipeline;
using MQTT.DataProcessor.Repositories;
using MQTT.DataProcessor.Services;
using MQTT.Library;

namespace MQTT.DataProcessor.Extentions
{
    public static class ServiceRegistrationExtention
    {
        public static void AddServiceCollections(this IServiceCollection services)
        {
            services.AddTransient<DataPipeline>()
                    .AddTransient<MQTTMessageConsumeHandler>()
                    .AddTransient<VehicleService>()
                    .AddTransient<TemperatureService>()
                    .AddTransient<ISource, MQTTSource>()
                    .AddTransient<KafkaProducer>()

                    .AddTransient<ITopicFactory, TopicFactory>()
                    .AddTransient<IExtractService, ExtractPayload>()

                    .AddTransient<ITemperatureRepository, TemperatureRepository>()
                    .AddTransient<IVehicleRepository, VehicleRepository>()
                    ;
        }
    }
}
