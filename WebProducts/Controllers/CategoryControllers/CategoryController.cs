using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebProducts.Application.Commands.CategoryCommands;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Models;

namespace WebProducts.Controllers.CategoryControllers;
[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("single")]
    public async Task<IActionResult> GetSingleCategory([FromQuery]IdModel model)
    {
        var result = await _mediator.Send(new GetCategoryCommand(model.Id));
        return Ok(result);
    }

    [HttpGet("CategoryTree")]
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
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(FullCategoryModel model)
    {
        var result = await _mediator.Send(new UpdateCategoryCommand(model.Id, model.Name));
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(IdModel model)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(model.Id));
        return Ok(result);
    }
    
    
}