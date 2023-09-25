

using Microsoft.Extensions.DependencyInjection;

namespace MQTT.DataProcessor.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IServiceScope _serviceScope;

        protected BaseRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Dispose()
        {
            _serviceScope?.Dispose();
        }

        //public AppDbContext GetDbContext()
        //{
        //    _serviceScope = _serviceScopeFactory.CreateScope();
        //    return _serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        //}
    }
}
