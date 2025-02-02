using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.CountryCommands;

public record GetCountriesCommand() : IRequest<List<Country>>{}

public class GetCountriesCommandHandler : IRequestHandler<GetCountriesCommand, List<Country>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetCountriesCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Country>> Handle(GetCountriesCommand request, CancellationToken cancellationToken)
    {
        var result = _countryRepository.Query().ToList();
        await _unitOfWork.SaveAsync(cancellationToken);
        return result;
    }
}