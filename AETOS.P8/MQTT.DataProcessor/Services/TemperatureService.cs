using Confluent.Kafka;
using Kafka.Library;
using KAFKA.Library;
using Microsoft.Extensions.Configuration;
using MQTT.DataProcessor.Repositories;
using Newtonsoft.Json;
using P8.Model.Models;

namespace MQTT.DataProcessor.Services
{
    public class TemperatureService : ITopicService
    {
        private Temperature _temperaturePayload;
        private ITemperatureRepository _temperatureRepository;
        private readonly KafkaProducer _kafkaProducer;
        private IConfiguration _config;
        public TemperatureService(ITemperatureRepository temperatureRepository, KafkaProducer kafkaProducer, IConfiguration configuration)
        {
            _kafkaProducer = kafkaProducer;
            _config = configuration;
        
        _temperatureRepository = temperatureRepository;
        }
        public ITopicService Initialize(string jsonPayload)
        {
            _temperaturePayload = JsonConvert.DeserializeObject<Temperature>(jsonPayload);
            return this;
        }

        public ITopicService Load()
        {
            var kAFKASettings = _config.GetSection("KAFKA").Get<KAFKASettings>();

            _kafkaProducer.Setup(kAFKASettings.BootstrapServers, kAFKASettings.GroupId);
            var data = JsonConvert.SerializeObject(_temperaturePayload);
            _kafkaProducer.Produce("temperature", "abdsfdsfd");

            return this;
        }

        public ITopicService Transform()
        {
            //other logic here

            return this;
        }
    }
}
