using MediatR;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Application.Commands.CategoryCommands;

public record GetCategoryCommand(int Id) : IRequest<Category>;

public class GetCategoryCommandHandler : IRequestHandler<GetCategoryCommand, Category>
{
    private readonly ICategoryRepository _repository;
    
    public GetCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Category> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.Find(request.Id);
        return result;
    }
}
