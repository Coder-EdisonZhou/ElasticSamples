using ElasticAppDemo.Host.Infrastructure.Contracts;
using ElasticAppDemo.Host.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElasticAppDemo.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class AppLogController : ControllerBase
{
	private readonly IAppLogRepository _appLogRepository;

	public AppLogController(IAppLogRepository appLogRepository)
	{
		_appLogRepository = appLogRepository;
	}

	[HttpGet]
	public async Task<IActionResult> QueryAsync(int page = 1, int limit = 10)
	{
		var result = await _appLogRepository.QueryAsync(page, limit);
		if (result.Item1 <= 0 || !result.Item2.Any())
			return NotFound();

		return Ok(new 
		{
			total = result.Item1,
			items = result.Item2
		});
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync([FromBody] AppLog log)
	{
		await _appLogRepository.AddAsync(log);
		return Ok("Success");
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAsync([Required] string id, [FromBody] AppLog log)
	{
		log.Id = id;
		await _appLogRepository.UpdateAsync(log);
		return Ok("Success");
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync([Required] string id)
	{
		await _appLogRepository.DeleteAsync(id);
		return Ok("Success");
	}
}