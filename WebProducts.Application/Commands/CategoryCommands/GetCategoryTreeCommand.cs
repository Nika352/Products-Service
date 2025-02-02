using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.CategoryCommands;

public record GetCategoryTreeCommand() : IRequest<List<Node>>{}

public class GetCategoryTreeCommandHandler : IRequestHandler<GetCategoryTreeCommand, List<Node>>
{
    private readonly ICategoryRepository _categoryRepository;
    
    public GetCategoryTreeCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Node>> Handle(GetCategoryTreeCommand request, CancellationToken cancellationToken)
    {
        var result = _categoryRepository.Query();
        var categories = result.ToList(); 
        var tree = BuildTree(categories); 
        
        return tree;
    }

    private static List<Node> BuildTree(List<Category> items, int? parentId = 0)
    {
        var tree = new List<Node>();
        var filteredTree = items.Where(x => x.ParentId == parentId).ToList();
        foreach (var item in filteredTree)
        {
            var children = BuildTree(items, item.Id);
            
            var node = new Node()
            {
                Id = item.Id,
                Name = item.Name,
                ParentId = item.ParentId,
                Children = children
            };

            tree.Add(node);
        }

        return tree;
    }
}

public class Node
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }

    public List<Node> Children { get; set; } = new List<Node>(); 
}