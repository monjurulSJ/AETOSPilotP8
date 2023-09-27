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
        public VehicleRepository(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {

        }
        public async Task<IList<Vehicle>> GetVehicles(DateTime startTime, DateTime endTime, int speed)
        {
            var appDbContext = GetDbContext();

            var vehicles = await appDbContext.Vehicles.Where(a => a.Timestamp <= startTime &&
                    a.Timestamp >= endTime && a.Speed == speed).ToListAsync();

            return vehicles;
        }

        public async Task<List<VehicleTemperature>> GetTemperatures(DateTime targetDate)
        {
            var appDbContext = GetDbContext();

            var vehicleTemperatures = new List<VehicleTemperature>();

            Console.WriteLine(targetDate.Date);

            var temperatures = appDbContext.Temperatures.Where(t => t.timestamp.Date == targetDate.Date)
                .GroupBy(t => new { t.DeviceId, t.timestamp.Hour })
                .Select(group => new
                {
                    deviceId = group.Key.DeviceId,
                    hour = group.Key.Hour,
                    averageTemperature = group.Average(r => r.temp)
                })
                .OrderBy(result => result.deviceId)
                .ThenBy(result => result.hour)
                .ToList();

            var hourlyAveragesDict = new Dictionary<(int Id, int Hour), double>();

            foreach (var result in temperatures)
            {
                hourlyAveragesDict[(result.deviceId, result.hour)] = result.averageTemperature;
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
