using Microsoft.Extensions.DependencyInjection;

namespace KAFKA.DataProcessor.Repositories
{
    public interface ITemperatureRepository
    {

    }

    public class TemperatureRepository : BaseRepository, ITemperatureRepository
    {
        public TemperatureRepository(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }

    }
}
