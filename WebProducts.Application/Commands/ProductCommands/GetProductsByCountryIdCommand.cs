using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.ProductCommands;

public record GetProductsByCountryIdCommand(int Id) : IRequest<List<Product>>;

public class GetProductsByCountryIdHandler : IRequestHandler<GetProductsByCountryIdCommand, List<Product>>
{
    private readonly IProductRepository _repository;

    public GetProductsByCountryIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> Handle(GetProductsByCountryIdCommand request, CancellationToken cancellationToken)
    {
        var result = _repository.Query(x => x.CountryId == request.Id);
        return result.ToList();
    }
    
}