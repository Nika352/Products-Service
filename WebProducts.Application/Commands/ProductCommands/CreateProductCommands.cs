using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.ProductCommands;

public record CreateProductCommand(int Code, string Name, decimal Price, int CategoryId, int CountryId) : IRequest<Product>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Code = request.Code,
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId,
            CountryId = request.CountryId
        };

        await _productRepository.Store(product);
        
        await _unitOfWork.SaveAsync(cancellationToken);

        return product;
    }
}