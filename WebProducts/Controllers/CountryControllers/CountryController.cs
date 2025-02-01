using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebProducts.Application.Commands.CountryCommands;
using WebProducts.Models;

namespace WebProducts.Controllers.CountryControllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CountryModel model)
    {
        var result = await _mediator.Send(new CreateCountryCommand(model.Name));
        return Ok(result);
    }
}