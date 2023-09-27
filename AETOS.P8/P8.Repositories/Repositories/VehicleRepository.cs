using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using P8.Model.DbContexts;
using P8.Model.Models;
using P8.Model.Responses;
using System.Globalization;

namespace P8.Repository.Repositories
{
    public interface IVehicleRepository
    {
        Task<IList<Vehicle>> GetVehicles(DateTime startTime, DateTime endTime, int speed);
        Task<List<VehicleTemperature>> GetTemperatures(DateTime targetDate);
    }

    public class VehicleRepository : BaseRepository, IVehicleRepository
    {
        private readonly AppDbContext _appDbContext;
        public VehicleRepository(IServiceScopeFactory serviceScopeFactory, AppDbContext appDbContext) : base(serviceScopeFactory)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IList<Vehicle>> GetVehicles(DateTime startTime, DateTime endTime, int speed)
        {
            var vehicles = await _appDbContext.Vehicles.Where(a => DateTime.Parse(a.Timestamp) <= startTime &&
                    DateTime.Parse(a.Timestamp) >= endTime && a.Speed == speed).ToListAsync();

            return vehicles;
        }

        public async Task<List<VehicleTemperature>> GetTemperatures(DateTime targetDate)
        {
            var vehicleTemperatures = new List<VehicleTemperature>();

            var temperatures = _appDbContext.Temperatures
                .Select(t => new
                {
                    id = t.id,
                    deviceId = t.DeviceId,
                    temp = t.temp,
                    psi = t.psi,
                    Timestamp = DateTime.Parse(t.timestamp)
                }).ToList();

            var hourlyAverages = temperatures
                .Where(t => t.Timestamp.Date == targetDate.Date)
                .GroupBy(t => new { t.deviceId, t.Timestamp.Hour })
                .Select(group => new
                {
                    Id = group.Key.deviceId,
                    Hour = group.Key.Hour,
                    AverageTemperature = group.Average(r => r.temp)
                })
                .OrderBy(result => result.Id)
                .ThenBy(result => result.Hour)
                .ToList();
             
            var hourlyAveragesDict = new Dictionary<(int Id, int Hour), double>();

            foreach (var result in hourlyAverages)
            {
                hourlyAveragesDict[(result.Id, result.Hour)] = result.AverageTemperature;
            }
             
            foreach (var deviceId in temperatures.Select(t => t.deviceId).Distinct())
            {
                var temp = new VehicleTemperature
                {
                    Id = deviceId,
                    Date = targetDate.Date,
                    HourlyAverages = new Dictionary<int, double>()
                };

                for (int hour = 0; hour < 24; hour++)
                {
                    if (hourlyAveragesDict.TryGetValue((deviceId, hour), out double average))
                    {
                        temp.HourlyAverages[hour] = average;
                    }
                    else
                    {
                        temp.HourlyAverages[hour] = 0.0;  
                    }
                }

                vehicleTemperatures.Add(temp);
            }

            return vehicleTemperatures;
        } 
    }
}
