using ElasticAppDemo.Host.Infrastructure.Contracts;
using ElasticAppDemo.Host.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElasticAppDemo.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
	private readonly IProductRepository _productRepository;

	public ProductController(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	[HttpGet]
	public async Task<IActionResult> QueryAsync(int page = 1, int limit = 10)
	{
		var result = await _productRepository.QueryAsync(page, limit);
		return Ok(new 
		{
			total = result.Item1,
			items = result.Item2
		});
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync([FromBody] Product product)
	{
		await _productRepository.AddAsync(product);
		return Ok("Success");
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAsync([FromBody] Product product)
	{
		await _productRepository.UpdateAsync(product);
		return Ok("Success");
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync([Required] string id)
	{
		await _productRepository.DeleteAsync(id);
		return Ok("Success");
	}
}