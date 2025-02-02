using System.Windows.Input;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.CountryCommands;

public record CreateCountryCommand(string Name) : IRequest<Country>;

public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Country>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Country> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
      var countries = _countryRepository.Query().ToList();
      if (countries.Any(x => x.Name == request.Name))
      {
          throw new InvalidOperationException($"Country with name '{request.Name}' already exists.");
      }
      
      var country = new Country()
      {
          Name = request.Name
      };
      
      await _countryRepository.Store(country);
      await _unitOfWork.SaveAsync(cancellationToken);
      
      return country;
    }

}