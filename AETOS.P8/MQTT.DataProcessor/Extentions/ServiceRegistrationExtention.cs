using Kafka.Library;
using Microsoft.Extensions.DependencyInjection;
using MQTT.DataProcessor.Connectors;
using MQTT.DataProcessor.Pipeline;
using MQTT.DataProcessor.Repositories;
using MQTT.DataProcessor.Services;
using MQTT.Library;
using System;

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
                    
                    .AddTransient<Func<string, ITopicService>>(serviceProvider => key =>
                    {
                        return key switch
                        {
                            "temperature" => serviceProvider.GetRequiredService<TemperatureService>(),
                            "vehicle" => serviceProvider.GetRequiredService<VehicleService>(),
                            _ => throw new NotImplementedException()
                        };
                    })

                     //.AddTransient<Func<string, ITopicService>>(serviceProvider => key =>HandleServiceResolver(serviceProvider,key))

                    ;
        }

        //private static ITopicService HandleServiceResolver(IServiceProvider serviceProvider, string key)
        //{
        //    return key switch
        //    {
        //        "temperature" => serviceProvider.GetRequiredService<TemperatureService>(),
        //        "vehicle" => serviceProvider.GetRequiredService<VehicleService>(),
        //        _ => throw new NotImplementedException()
        //    };
        //}
    }

}
