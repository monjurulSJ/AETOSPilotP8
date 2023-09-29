using AutoMapper;
using Confluent.Kafka;
using Kafka.Library;
using KAFKA.Library;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using P8.Model.DTO;
using P8.Model.Models;
using P8.Repository.Repositories;

namespace KAFKA.DataProcessor.Services
{
    public class VehicleService : ITopicService
    {
        private VehicleDTO _vehicleDTO;
        private Vehicle _vehicle;
        public readonly IMapper _mapper;

        private IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
        }
        public ITopicService Initialize(string jsonPayload)
        {
            _vehicleDTO = JsonConvert.DeserializeObject<VehicleDTO>(jsonPayload);
            return this;
        }

        public ITopicService Load()
        {
            _vehicleRepository.SaveVehicleInfo(_vehicle);
            return this;
        }

        public ITopicService Transform()
        {
            _vehicle = _mapper.Map<Vehicle>(_vehicleDTO); //destination then source
            _vehicle.CreatedAt = DateTime.Now.ToUniversalTime();
            _vehicle.Timestamp = _vehicleDTO.Timestamp.ToUniversalTime();
            return this;
        }
    }
}
