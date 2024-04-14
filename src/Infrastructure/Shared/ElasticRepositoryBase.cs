using Nest;

namespace ElasticAppDemo.Host.Infrastructure.Shared;

public abstract class ElasticRepositoryBase<T> : IElasticRepositoryBase<T> where T : ElasticModelBase
{
	private readonly IElasticProxy _elasticProxy;

	public ElasticRepositoryBase(IElasticProxy elasticProxy)
	{
		_elasticProxy = elasticProxy;
	}

	protected abstract string IndexName { get; }
	protected IElasticClient Client => _elasticProxy.GetClient(IndexName);

	public virtual async Task AddAsync(T item)
	{
		await this.Client.IndexAsync(item, x => x.Index(this.IndexName));
	}

	public virtual async Task AddManyAsync(T[] items)
	{
		await this.Client.IndexManyAsync(items, this.IndexName);
	}

	public virtual async Task DeleteAsync(string id)
	{
		await this.Client.DeleteAsync<T>(id, x => x.Index(this.IndexName));
	}

	public virtual async Task UpdateAsync(T item)
	{
		await this.Client.UpdateAsync<T>(item.Id, x => x.Index(this.IndexName).Doc(item));
	}

	public virtual async Task<Tuple<int, IList<T>>> QueryAsync(int page, int limit)
	{
		var query = await this.Client.SearchAsync<T>(x => x.Index(this.IndexName)
			.From((page -1) * limit)
			.Size(limit));

		return new Tuple<int, IList<T>>(Convert.ToInt32(query.Total), query.Documents.ToList());
	}
}
