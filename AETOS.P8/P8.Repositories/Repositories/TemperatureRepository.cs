using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using P8.Model.Models;
using P8.Model.Responses;

namespace P8.Repository.Repositories
{
    public interface ITemperatureRepository
    {
        Task<List<VehicleTemperature>> GetTemperatures(DateTime targetDate);
        Task<List<Temperature>>  GetAllTemperatures();
        Task<DeviceInfo> GetDeviceInfoByDeviceId(int deviceId);
        Task<List<VehicleMaxMinTemperature>> GetMaximumAndMinimumTemperature(DateTime targetDate);
        Task<Temperature> SaveVehicleTemperature(Temperature vehicleTemperature);
    }

    public class TemperatureRepository : BaseRepository, ITemperatureRepository
    {
        public TemperatureRepository(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }
        public async Task<List<VehicleTemperature>> GetTemperatures(DateTime targetDate)
        {
            var appDbContext = GetDbContext();

            var vehicleTemperatures = new List<VehicleTemperature>();

            var temperatures = await appDbContext.Temperatures.Where(t => t.Timestamp.Date == targetDate.Date)
                                    .GroupBy(t => new { t.DeviceInfoId, t.Timestamp.Hour })
                                    .Select(group => new
                                    {
                                        deviceId = group.Key.DeviceInfoId,
                                        hour = group.Key.Hour,
                                        averageTemperature = group.Average(r => r.Temp)
                                    })
                                    .OrderBy(result => result.deviceId)
                                    .ThenBy(result => result.hour)
                                    .ToListAsync();

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
                        temp.HourlyAverages[hour] = Math.Round(average, 2, MidpointRounding.AwayFromZero);
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
        public async Task<List<VehicleMaxMinTemperature>> GetMaximumAndMinimumTemperature(DateTime targetDate)
        {
            var appDbContext = GetDbContext();

            var result = await appDbContext.Temperatures.Where(t => t.Timestamp.Date == targetDate.Date)
                                .GroupBy(t => t.DeviceInfoId)
                                .Select(t => new VehicleMaxMinTemperature
                                {
                                    DeviceId = t.Key,
                                    TargetDateTime =  targetDate,
                                    MaxTemperature = t.Max(a => a.Temp),
                                    MinTemperature = t.Min(a => a.Temp)
                                })
                                .ToListAsync();

            return result;
            
        }
        public async Task<Temperature> SaveVehicleTemperature(Temperature vehicleTemperature)
        {
            try
            {
               var db = GetDbContext();
               await  db.AddAsync(vehicleTemperature);

               await db.SaveChangesAsync();
          
               return vehicleTemperature;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<Temperature>> GetAllTemperatures()
        {
             var db = GetDbContext();
             var data= db.Temperatures.ToListAsync();

             return await data;
        }
        public async Task<DeviceInfo> GetDeviceInfoByDeviceId(int deviceId)
        {
            var db = GetDbContext();

            DeviceInfo deviceInfo = await db.DeviceInfos.FirstOrDefaultAsync(x=>x.DeviceId==deviceId);

            return  deviceInfo;
        } 
    }
}
