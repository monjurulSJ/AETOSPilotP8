using Microsoft.Extensions.DependencyInjection;

namespace P8.Repository.Repositories
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
