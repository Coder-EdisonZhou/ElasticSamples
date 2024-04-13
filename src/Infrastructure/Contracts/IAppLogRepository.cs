using ElasticAppDemo.Host.Infrastructure.Shared;
using ElasticAppDemo.Host.Models;

namespace ElasticAppDemo.Host.Infrastructure.Contracts;

public interface IAppLogRepository : IElasticRepositoryBase<AppLog>
{
}
