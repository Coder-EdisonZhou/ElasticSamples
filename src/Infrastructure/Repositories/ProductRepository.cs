using ElasticAppDemo.Host.Infrastructure.Contracts;
using ElasticAppDemo.Host.Infrastructure.Shared;
using ElasticAppDemo.Host.Models;

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
}
