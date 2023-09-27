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
    }
    public class AetosService : IAetosService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public AetosService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        } 
        public async Task<IList<Vehicle>> GetVehicles(DateTime startTime, DateTime endTime, int speed)
        {
            var vehicles = await _vehicleRepository.GetVehicles(startTime, endTime, speed);   

            return vehicles;
        } 
        public async Task<IList<VehicleTemperature>> GetTemperatures(DateTime targetDate)
        {
            var temperatures = await _vehicleRepository.GetTemperatures(targetDate);

            return temperatures;
        }
    }
}
