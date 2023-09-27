using P8.Repository.Repositories;
using P8.Service.Services;

namespace AETOS.P8.App.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            return services
               .AddScoped<IAetosService, AetosService>()
               .AddScoped<ITemperatureRepository, TemperatureRepository>()
               .AddScoped<IVehicleRepository, VehicleRepository>();
        }
    }
}
