using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebProducts.Infrastructure.Persistence;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories.Repository;

namespace WebProducts.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
}

public class ProductRepository : IProductRepository
{
    private readonly ProductsDbContext _dbContext;
    
    public ProductRepository(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Product> Find(int id)
    {
        return await _dbContext.Products.FirstAsync(x => x.Id == id);
    }

    public IQueryable<Product> Query(Expression<Func<Product, bool>>? expression = null)
    {
        return expression != null ? 
            _dbContext.Products.Where(expression) :
            _dbContext.Products.AsQueryable();
    }

    public async Task Store(Product document)
    {
        await _dbContext.Products.AddAsync(document);
    }

    public void Delete(Product document)
    {
        _dbContext.Products.Remove(document);
    }
}