using ElasticAppDemo.Host.Infrastructure.Shared;

namespace ElasticAppDemo.Host.Models;

public class AppLog : ElasticModelBase
{
	public override string Id => Guid.NewGuid().ToString();
	public string LogLevel { get; set; } = "Info";
	public string Message { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}
