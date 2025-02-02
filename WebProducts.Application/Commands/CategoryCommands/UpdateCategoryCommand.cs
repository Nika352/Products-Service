using MediatR;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.CategoryCommands;

public record UpdateCategoryCommand(int Id, string Name) : IRequest<bool>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
       var result = await _categoryRepository.Find(request.Id);
       result.Name = request.Name;
       await _unitOfWork.SaveAsync(cancellationToken);
       return true;
    }
}
