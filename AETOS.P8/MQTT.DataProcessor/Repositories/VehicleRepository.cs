using Microsoft.Extensions.DependencyInjection;

namespace MQTT.DataProcessor.Repositories
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
