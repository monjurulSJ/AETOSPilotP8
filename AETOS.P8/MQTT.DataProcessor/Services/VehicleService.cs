using Confluent.Kafka;
using Kafka.Library;
using KAFKA.Library;
using Microsoft.Extensions.Configuration;
using MQTT.DataProcessor.Repositories;
using Newtonsoft.Json;
using P8.Model.DTO;
using P8.Model.Models;

namespace MQTT.DataProcessor.Services
{
    public class VehicleService : ITopicService
    {
        private VehicleDTO _vehiclePayload;
        private IVehicleRepository _vehicleRepository;
        private readonly KafkaProducer _kafkaProducer;
        private IConfiguration _config;
        public VehicleService(IVehicleRepository vehicleRepository,KafkaProducer kafkaProducer,IConfiguration configuration)
        {
            _vehicleRepository = vehicleRepository;
            _kafkaProducer = kafkaProducer;
            _config = configuration;
        }
        public ITopicService Initialize(string jsonPayload)
        {
            _vehiclePayload = JsonConvert.DeserializeObject<VehicleDTO>(jsonPayload);
            return this;
        }

        public ITopicService Load()
        {
            var kAFKASettings = _config.GetSection("KAFKA").Get<KAFKASettings>();

            _kafkaProducer.Setup(kAFKASettings.BootstrapServers, kAFKASettings.GroupId);
            var data=JsonConvert.SerializeObject(_vehiclePayload);
            _kafkaProducer.Produce("vehicle",data);

            return this;
        }

        public ITopicService Transform()
        {

            return this;
        }
    }
}
