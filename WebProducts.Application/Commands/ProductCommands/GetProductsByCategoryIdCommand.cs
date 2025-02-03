using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.ProductCommands;

public record GetProductsByCategoryIdCommand (int Id) : IRequest<List<Product>> {}

public class GetProductsByCategoryIdCommandHandler : IRequestHandler<GetProductsByCategoryIdCommand, List<Product>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    public GetProductsByCategoryIdCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Product>> Handle(GetProductsByCategoryIdCommand request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.Query().ToList();
        var childCategoriesList = ChildCategories(categories, request.Id);
        childCategoriesList.Add(request.Id);
        var result = _productRepository.Query(x => childCategoriesList.Contains(x.CategoryId));
        return result.ToList();
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
