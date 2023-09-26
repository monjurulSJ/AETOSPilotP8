using Microsoft.Extensions.DependencyInjection;

namespace P8.Repository.Repositories
{
    public interface IVehicleRepository
    {

    }

    public class VehicleRepository : BaseRepository, IVehicleRepository
    {
        public VehicleRepository(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }
    }
}
