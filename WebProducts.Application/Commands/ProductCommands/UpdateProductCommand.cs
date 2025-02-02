using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.ProductCommands;

public record UpdateProductCommand(int Id, int Code, string Name, decimal Price, int CountryId, DateTime CreatedAt, DateTime EndDate) : IRequest<bool>{}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToChange = await _productRepository.Find(request.Id);
        productToChange.Code = request.Code;
        productToChange.Name = request.Name;
        productToChange.Price = request.Price;
        productToChange.CountryId = request.CountryId;
        productToChange.CreatedAt = request.CreatedAt;
        productToChange.EndDate = request.EndDate;
        await _unitOfWork.SaveAsync(cancellationToken);
        return true;
    }
}