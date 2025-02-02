using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebProducts.Application.Commands.ProductCommands;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Models;

namespace WebProducts.Controllers.ProductControllers;

[ApiController]
[Route("[Controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ByProductId")]
    public async Task<IActionResult> Get([FromQuery]IdModel model)
    {
        var result = await _mediator.Send(new GetProductCommand(model.Id));
        return Ok(result);
    }

    [HttpGet("ByCategoryId")]
    public async Task<IActionResult> GetProductsByCategoryId([FromQuery] IdModel model)
    {
        var result = await _mediator.Send((new GetProductsByCategoryIdCommand(model.Id)));
        return Ok(result);
    }
    
    [HttpGet("ByCountryId")]
    public async Task<IActionResult> GetProductsByCountryId([FromQuery] IdModel model)
    {
        var result = await _mediator.Send(new GetProductsByCountryIdCommand(model.Id));
        return Ok(result);
        
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAsync(ProductModel model)
    {
        var result = await _mediator.Send(new CreateProductCommand(model.Code, model.Name, model.Price, model.CategoryId, model.CountryId));
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(IdModel model)
    {
        var result = await _mediator.Send(new DeleteProductCommand(model.Id));
        return Ok(result);
    }
    
}