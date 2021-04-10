using DataAccessLayer;
using DataAccessLayer.Credentials;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Infra
{
    public static class BllInjectionModule
    {
        public static void Configure(IServiceCollection services, string apiKey)
        {
            services.AddScoped<IMapApiKeyProvider>(x => new MapApiKeyProvider(apiKey));
            services.AddScoped<IDataRetriver, DataRetriever>();
            services.AddScoped<IDataService, DataService>();
        }
    }
}
