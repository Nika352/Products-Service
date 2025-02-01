using System.Windows.Input;
using MediatR;
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
    public Task<Country> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = new Country()
        {
            Name = request.Name
        };
        _countryRepository.Store(country);
        _unitOfWork.SaveAsync(cancellationToken);
        
        return Task.FromResult(country);
    }
    
}