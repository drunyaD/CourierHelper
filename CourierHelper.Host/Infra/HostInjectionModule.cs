using BusinessLogic.Calculation;
using BusinessLogic.Services;
using CourierHelper.Host.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace CourierHelper.Host.Infra
{
    public static class HostInjectionModule
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IAlgorithmConfigProvider, AlgorithmConfigProvider>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IOptimizedRouteRequestValidator, OptimizedRouteRequestValidator>();
        }
    }
}
