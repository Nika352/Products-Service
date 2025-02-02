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
        return await _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Country)
            .FirstAsync(x => x.Id == id);
    }

    public IQueryable<Product> Query(Expression<Func<Product, bool>>? expression = null)
    {
        var query = _dbContext.Products
            .Include(x => x.Category)  
            .Include(x => x.Country);  

        return expression != null ? query.Where(expression) : query;
    }

    public async Task Store(Product document)
    {
        await _dbContext.Products.AddAsync(document);
    }

    public void Delete(int id)
    {
        var productToDelete = _dbContext.Products.FirstOrDefault(x => x.Id == id);
        if (productToDelete != null)
            _dbContext.Products.Remove(productToDelete);
    }
}