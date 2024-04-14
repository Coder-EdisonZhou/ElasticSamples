using ElasticAppDemo.Host.Infrastructure.Contracts;
using ElasticAppDemo.Host.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
		if (result.Item1 <= 0 || !result.Item2.Any())
			return NotFound();

		return Ok(new 
		{
			total = result.Item1,
			items = result.Item2
		});
	}

	[HttpGet("ean")]
	public async Task<IActionResult> QueryByEanAsync(string ean)
	{
		var result = await _productRepository.QueryByEanAsync(ean);
		if (result == null || !result.Any())
			return NotFound();

		return Ok(result);
	}

	[HttpGet("agg-demo1")]
	public async Task<IActionResult> QueryPriceAggAsync()
	{
		var queryResult = await _productRepository.QueryPriceAggAsync();
		if (queryResult.Count <= 0)
			return NotFound();

		return Ok(queryResult);
	}

	[HttpGet("agg-demo2")]
	public async Task<IActionResult> QueryBrandAggAsync()
	{
		var queryResult = await _productRepository.QueryBrandAggAsync();
		if (queryResult.Count <= 0)
			return NotFound();

		return Ok(queryResult);
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