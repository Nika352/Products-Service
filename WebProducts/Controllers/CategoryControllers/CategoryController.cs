using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebProducts.Application.Commands.CategoryCommands;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Models;

namespace WebProducts.Controllers.CategoryControllers;
[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleCategory(int id)
    {
        var result = await _mediator.Send(new GetCategoryCommand(id));
        return Ok(result);
    }

    [HttpGet("category-tree")]
    public async Task<IActionResult> GetCategoryTree()
    {
        var result = await _mediator.Send(new GetCategoryTreeCommand());
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAsync(CategoryModel model)
    {
        var result = await _mediator.Send(new CreateCategoryCommand(model.Name, model.ParentId));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] Dictionary<string, string> body)
    {
        var name = body["name"];
        var result = await _mediator.Send(new UpdateCategoryCommand(id, name));
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));
        return Ok(result);
    }
    
}
