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

            var vehicles = await appDbContext.Vehicles.Where(a => a.Timestamp.Date >= startTime.Date &&
                    a.Timestamp.Date <= endTime.Date && a.Speed > speed).ToListAsync();

            return vehicles;
        } 
    }
}
