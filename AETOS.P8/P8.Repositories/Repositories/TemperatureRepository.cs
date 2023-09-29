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
        Task<List<VechileSpeed>> GetMaxMinSpeedHour(DateTime startDate,DateTime endDate);
        
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

        public async Task<List<VechileSpeed>> GetMaxMinSpeedHour(DateTime startDate, DateTime endDate)
        {
            var appDbContext = GetDbContext();

            var vehicleSpeed = new List<VechileSpeed>();

            var result = await appDbContext.Vehicles
                                    .Where(t => (t.Timestamp.Date>= startDate.Date 
                                     && t.Timestamp.Date<=endDate.Date))
                                    .GroupBy(t => new { t.DriverID, t.Timestamp.Hour,t.Timestamp.Date })
                                    .Select(group => new
                                    {
                                        date= group.Key.Date,
                                        deviceId = group.Key.DriverID,
                                        hour = group.Key.Hour,
                                        maxSpeed = group.Max(r => r.Speed),
                                        minSpeed = group.Min(r => r.Speed),
                                    })
                                    .OrderBy(result => result.deviceId)
                                    .ThenBy(result => result.hour)
                                    .ToListAsync();

            var hourlyMaxDict = new Dictionary<(int Id, int Hour), double>();
            var hourlyMinDict = new Dictionary<(int Id, int Hour), double>();

            foreach (var item in result)
            {
                hourlyMaxDict[(item.deviceId, item.hour)] = item.maxSpeed;
                hourlyMinDict[(item.deviceId, item.hour)] = item.minSpeed;

            }

            foreach (var item in result.Distinct())
            {
                var temp = new VechileSpeed
                {
                    DeviceId = item.deviceId,
                    Date = item.date,
                    HourlyMax = new Dictionary<int, double>(),
                    HourlyMin = new Dictionary<int, double>(),
                };

                for (int hour = 0; hour < 24; hour++)
                {
                    if (hourlyMaxDict.TryGetValue((item.deviceId, hour), out double max))
                    {
                        temp.HourlyMax[hour] = Math.Round(max, 2, MidpointRounding.AwayFromZero);
                    }
                    if (hourlyMinDict.TryGetValue((item.deviceId, hour), out double min))
                    {
                        temp.HourlyMin[hour] = Math.Round(min, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        temp.HourlyMax[hour] = 0.0;
                        temp.HourlyMin[hour] = 0.0;
                    }
                }
                vehicleSpeed.Add(temp);
            }
            return vehicleSpeed;
        }


    }
}
