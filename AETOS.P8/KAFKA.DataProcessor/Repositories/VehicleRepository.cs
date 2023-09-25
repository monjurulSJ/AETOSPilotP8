using Microsoft.Extensions.DependencyInjection;

namespace KAFKA.DataProcessor.Repositories
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
