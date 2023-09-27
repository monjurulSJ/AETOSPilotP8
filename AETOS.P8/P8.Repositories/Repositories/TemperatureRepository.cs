using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace P8.Repository.Repositories
{
    public interface ITemperatureRepository
    {
        int add();
    }

    public class TemperatureRepository : BaseRepository, ITemperatureRepository
    {
        public TemperatureRepository(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }
        public int add() {
            return 0;
        }
    }
}
