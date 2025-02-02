using MediatR;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.ProductCommands;

public record DeleteProductCommand(int Id) : IRequest<int>{}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _repository.Delete(request.Id);
        await _unitOfWork.SaveAsync(cancellationToken);
        return request.Id;
    }
}
