using P8.Model.Models;
using P8.Model.Responses;
using P8.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Service.Services
{
    public interface IAetosService
    {
        Task<IList<Vehicle>> GetVehicles(DateTime startTime, DateTime endTime, int speed);
        Task<IList<VehicleTemperature>> GetTemperatures(DateTime targetDate);
        Task<IList<VehicleMaxMinTemperature>> GetVehicleMaxMinTemperatures(DateTime targetDate);

        Task<IList<VechileSpeed>> GetMaxMinSpeedHour(DateTime startDate, DateTime endDate);
    }
    public class AetosService : IAetosService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITemperatureRepository _temperaturesRepository;
        public AetosService(IVehicleRepository vehicleRepository, ITemperatureRepository temperatureRepository)
        {
            _temperaturesRepository = temperatureRepository;
            _vehicleRepository = vehicleRepository;
        } 
        public async Task<IList<Vehicle>> GetVehicles(DateTime startTime, DateTime endTime, int speed)
        {
            var vehicles = await _vehicleRepository.GetVehicles(startTime, endTime, speed);   

            return vehicles;
        } 
        public async Task<IList<VehicleTemperature>> GetTemperatures(DateTime targetDate)
        {
            var temperatures = await _temperaturesRepository.GetTemperatures(targetDate);

            return temperatures;
        }
        public async Task<IList<VehicleMaxMinTemperature>> GetVehicleMaxMinTemperatures(DateTime targetDate)
        {
            var temperatures = await _temperaturesRepository.GetMaximumAndMinimumTemperature(targetDate);

            return temperatures;
        }
        public async Task<IList<VechileSpeed>> GetMaxMinSpeedHour(DateTime startDate,DateTime endDate)
        {
            var temperatures = await _temperaturesRepository.GetMaxMinSpeedHour(startDate, endDate);

            return temperatures;
        }


    }
}
