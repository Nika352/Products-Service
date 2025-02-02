using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;
using WebProducts.Infrastructure.Repositories.Repository;

namespace WebProducts.Application.Commands.CategoryCommands;

public record CreateCategoryCommand(String Name, int ParentId) : IRequest<Category>{}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category()
        {
            Name = request.Name,
            ParentId = request.ParentId
        };
        
        await _categoryRepository.Store(category);
        await _unitOfWork.SaveAsync(cancellationToken);

        return category;
    }
}
