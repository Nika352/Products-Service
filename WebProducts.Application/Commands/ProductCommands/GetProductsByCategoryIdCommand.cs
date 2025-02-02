using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.ProductCommands;

public record GetProductsByCategoryIdCommand (int Id) : IRequest<List<Product>> {}

public class GetProductsByCategoryIdCommandHandler : IRequestHandler<GetProductsByCategoryIdCommand, List<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryIdCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> Handle(GetProductsByCategoryIdCommand request, CancellationToken cancellationToken)
    {
        var result = _productRepository.Query(x => x.CategoryId == request.Id);
        return result.ToList();
    }
}
