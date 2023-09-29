using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using P8.Model.Models;
using P8.Model.Responses;

namespace P8.Repository.Repositories
{
    public interface ITemperatureRepository
    {
        Task<Temperature> SaveVehicleTemperature(Temperature vehicleTemperature);

        Task<List<Temperature>>  GetAllTemperatures();
        Task<DeviceInfo> GetDeviceInfoByDeviceId(int deviceId);
    }

    public class TemperatureRepository : BaseRepository, ITemperatureRepository
    {
        public TemperatureRepository(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

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

            DeviceInfo deviceInfo =await db.DeviceInfos.FirstOrDefaultAsync(x=>x.DeviceId==deviceId);

            return  deviceInfo;
        }


    }
}
