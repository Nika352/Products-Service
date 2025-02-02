using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.CountryCommands;


public record DeleteCountryCommand(int Id) : IRequest<int>{}

public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, int>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        _countryRepository.Delete(request.Id);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return request.Id;
    }
    
}