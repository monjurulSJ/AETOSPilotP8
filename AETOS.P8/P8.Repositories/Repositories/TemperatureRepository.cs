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

        Task<List<Temperature>> GetAllTemperatures();
    }

    public class TemperatureRepository : BaseRepository, ITemperatureRepository
    {
        public TemperatureRepository(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }
        public async Task<Temperature> SaveVehicleTemperature(Temperature vehicleTemperature)
        {
            var db = GetDbContext();

            await db.AddAsync(vehicleTemperature);

            return vehicleTemperature;
        }
        public async Task<List<Temperature>> GetAllTemperatures()
        {
             var db = GetDbContext();
             var data= db.Temperatures.ToListAsync();

             return await data;
        }
    }
}
