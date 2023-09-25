using Confluent.Kafka;
using Kafka.Library;
using KAFKA.DataProcessor.Models;
using KAFKA.DataProcessor.Repositories;
using KAFKA.Library;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KAFKA.DataProcessor.Services
{
    public class VehicleService : ITopicService
    {
        private Vehicle _vehiclePayload;
        private IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public ITopicService Initialize(string jsonPayload)
        {
            _vehiclePayload = JsonConvert.DeserializeObject<Vehicle>(jsonPayload);
            return this;
        }

        public ITopicService Load()
        {

            return this;
        }

        public ITopicService Transform()
        {

            return this;
        }
    }
}
