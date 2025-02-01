using WebProducts.Infrastructure.Persistence;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories.Repository;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebProducts.Infrastructure.Repositories;

public class CategoryRepository : IRepository<Category>
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

    public void Delete(Category document)
    {
        _dbContext.Categories.Remove(document);
    }
}