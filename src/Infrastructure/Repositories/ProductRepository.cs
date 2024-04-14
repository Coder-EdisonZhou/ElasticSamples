using ElasticAppDemo.Host.Infrastructure.Contracts;
using ElasticAppDemo.Host.Infrastructure.Shared;
using ElasticAppDemo.Host.Models;
using System.Linq;

namespace ElasticAppDemo.Host.Infrastructure.Repositories;

public class ProductRepository : ElasticRepositoryBase<Product>, IProductRepository
{
    public ProductRepository(IElasticProxy elasticProxy) : base(elasticProxy)
    {
    }

	protected override string IndexName => "products";

	public override async Task<Tuple<int, IList<Product>>> QueryAsync(int page, int limit)
	{
		var query = await this.Client.SearchAsync<Product>(x => x.Index(this.IndexName)
			.From((page - 1) * limit)
			.Size(limit)
			.Sort(x => x.Descending(v => v.ReleaseDate)));

		return new Tuple<int, IList<Product>>(Convert.ToInt32(query.Total), query.Documents.ToList());
	}

	public async Task<IList<Product>> QueryByEanAsync(string ean)
	{
		var result = await this.Client.SearchAsync<Product>(x => x.Index(this.IndexName)
			.Query(q => q.Term(p => p.Ean, ean)));
		return result.Documents.ToList();
	}

	public async Task<Nest.AggregateDictionary> QueryPriceAggAsync()
	{
		var searchResult = await this.Client.SearchAsync<Product>(x => x.Index(this.IndexName)
			.Size(0) // 代表不返回源数据
			.Aggregations(agg => agg.Average("price_avg", avg => avg.Field("price"))
					.Max("price_max", max => max.Field("price"))
					.Min("price_min", min => min.Field("price")))
			);
		return searchResult.Aggregations;
	}

	public async Task<Nest.AggregateDictionary> QueryBrandAggAsync()
	{
		var searchResult = await this.Client.SearchAsync<Product>(x => x.Index(this.IndexName)
			.Size(0) // 代表不返回源数据
			.Aggregations(agg => agg.Terms("brandgroup", group => group.Field("brand"))
			));
		return searchResult.Aggregations;
	}
}
