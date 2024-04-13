using ElasticAppDemo.Host.Infrastructure.Contracts;
using ElasticAppDemo.Host.Infrastructure.Repositories;

namespace ElasticAppDemo.Host.Infrastructure.Extensions;

public static class InfraServiceExtensions
{
    public static void AddApplicationInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IElasticProxy, ElasticProxy>();
        services.AddSingleton<IProductRepository, ProductRepository>();
		services.AddSingleton<IAppLogRepository, AppLogRepository>();
	}
}
