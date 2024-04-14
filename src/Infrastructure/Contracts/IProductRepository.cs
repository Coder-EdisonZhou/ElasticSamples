using ElasticAppDemo.Host.Infrastructure.Shared;
using ElasticAppDemo.Host.Models;

namespace ElasticAppDemo.Host.Infrastructure.Contracts;

public interface IProductRepository : IElasticRepositoryBase<Product>
{
	Task<IList<Product>> QueryByEanAsync(string ean);
	Task<Nest.AggregateDictionary> QueryPriceAggAsync();
	Task<Nest.AggregateDictionary> QueryBrandAggAsync();
}
