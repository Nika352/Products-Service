using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebProducts.Application.Commands.ProductCommands;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Models;

namespace WebProducts.Controllers.ProductControllers;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetProductCommand(id));
        return Ok(result);
    }

    [HttpGet("category/{id}")]
    public async Task<IActionResult> GetProductsByCategoryId(int id)
    {
        var result = await _mediator.Send(new GetProductsByCategoryIdCommand(id));
        return Ok(result);
    }
    
    [HttpGet("country/{id}")]
    public async Task<IActionResult> GetProductsByCountryId(int id)
    {
        var result = await _mediator.Send(new GetProductsByCountryIdCommand(id));
        return Ok(result);
        
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAsync(FullProductModel model)
    {
        var result = await _mediator.Send(new CreateProductCommand(model.Code, model.Name, model.Price, model.CategoryId, model.CountryId, model.CreatedAt, model.EndDate));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id,  [FromBody]EditProductModel model)
    {
        var result = await _mediator.Send(new UpdateProductCommand(id, model.Code, model.Name, model.Price, model.CountryId, model.CreatedAt, model.EndDate));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        return Ok(result);
    }
    
}