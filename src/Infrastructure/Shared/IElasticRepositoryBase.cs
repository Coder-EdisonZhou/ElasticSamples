namespace ElasticAppDemo.Host.Infrastructure.Shared;

public interface IElasticRepositoryBase<T> where T : ElasticModelBase
{
    Task AddAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(string id);
    Task<Tuple<int, IList<T>>> QueryAsync(int page, int limit);
}
