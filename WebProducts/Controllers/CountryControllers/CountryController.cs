using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebProducts.Application.Commands.CountryCommands;
using WebProducts.Models;

namespace WebProducts.Controllers.CountryControllers;

[ApiController]
[Route("country")]
public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<CountryModel>> GetCountries()
    {
        var result = await _mediator.Send(new GetCountriesCommand());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CountryModel model)
    {
        var result = await _mediator.Send(new CreateCountryCommand(model.Name));
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCountryCommand(id));
        return Ok(result);
    }
}