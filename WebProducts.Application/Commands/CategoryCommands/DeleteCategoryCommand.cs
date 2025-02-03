using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;
using WebProducts.Infrastructure.Repositories.Repository;

namespace WebProducts.Application.Commands.CategoryCommands;

public record DeleteCategoryCommand(int Id) : IRequest<int>{}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, int>
{
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categories = _repository.Query().ToList();
        var categoriesToDelete = ChildCategories(categories, request.Id); 
        foreach (var id in categoriesToDelete)
        {
            _repository.Delete(id);
        }
        
        _repository.Delete(request.Id);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return request.Id;
    }

    private static List<int> ChildCategories(List<Category> categories, int parentId)
    {
        var result = new List<int>();
        var children = categories.Where(c => c.ParentId == parentId).ToList();
        foreach (var child in children)
        {
            result.Add(child.Id);
            result.AddRange(ChildCategories(categories, child.Id));  
        }
        
        return result;
    }
}