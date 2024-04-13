using ElasticAppDemo.Host.Infrastructure.Contracts;
using ElasticAppDemo.Host.Infrastructure.Shared;
using ElasticAppDemo.Host.Models;

namespace ElasticAppDemo.Host.Infrastructure.Repositories;

public class AppLogRepository : ElasticRepositoryBase<AppLog>, IAppLogRepository
{
    public AppLogRepository(IElasticProxy elasticProxy) : base(elasticProxy)
    {
    }

	protected override string IndexName => "app-logs";
}
