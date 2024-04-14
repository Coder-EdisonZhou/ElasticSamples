using ElasticAppDemo.Host.Infrastructure.Shared;
using Nest;

namespace ElasticAppDemo.Host.Models;

public class Product : ElasticModelBase
{
    public string Ean { get; set; }
    public string Name { get; set; }
	public string Description { get; set; }
	public string Brand { get; set; }
	public string Category { get; set; }
    public decimal Price { get; set; } = 0M;
	public int Quantity { get; set; } = 1;
    public DateTime ReleaseDate { get; set; } = DateTime.Now;

	public Product()
	{
		this.Id = Guid.NewGuid().ToString();
	}

	public Product(string id)
	{
		this.Id = id;
	}
}
