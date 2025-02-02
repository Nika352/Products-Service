using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;
using WebProducts.Infrastructure.Repositories.Repository;

namespace WebProducts.Application.Commands.ProductCommands;

public record GetProductCommand(int Id) : IRequest<Product>{}

public class GetProductCommandsHandler : IRequestHandler<GetProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductCommandsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.Find(request.Id);
        
        return result;
    }
}
