using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebProducts.Application.Commands.ProductCommands;
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

    [HttpPost]
    public async Task<IActionResult> AddAsync(ProductModel model)
    {
        
        var result = await _mediator.Send(new CreateProductCommand(model.Code, model.Name, model.Price, model.CategoryId, model.CountryId));
        
        return Ok(result);
    }
}