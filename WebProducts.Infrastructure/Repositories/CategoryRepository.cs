using WebProducts.Infrastructure.Persistence;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories.Repository;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebProducts.Infrastructure.Repositories;

public interface ICategoryRepository : IRepository<Category>{}

public class CategoryRepository : ICategoryRepository
{
    private readonly ProductsDbContext _dbContext;

    public CategoryRepository(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> Find(int id)
    {
        return await _dbContext.Categories.FirstAsync(x => x.Id == id);
    }

    public IQueryable<Category> Query(Expression<Func<Category, bool>>? expression = null)
    {
        return expression != null ? 
            _dbContext.Categories.Where(expression) :
            _dbContext.Categories.AsQueryable();
    }

    public async Task Store(Category document)
    {
        await _dbContext.Categories.AddAsync(document);
    }

    public void Delete(int id)
    {
        var categoryToDelete = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
        if (categoryToDelete != null)
            _dbContext.Categories.Remove(categoryToDelete);
    }
}