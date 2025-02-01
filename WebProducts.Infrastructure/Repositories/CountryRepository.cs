using Microsoft.EntityFrameworkCore;
using WebProducts.Infrastructure.Persistence;
using WebProducts.Infrastructure.Persistence.Entities;
using WebProducts.Infrastructure.Repositories.Repository;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace WebProducts.Infrastructure.Repositories;

public interface ICountryRepository : IRepository<Country>{}

public class CountryRepository : ICountryRepository
{
    private readonly ProductsDbContext _dbContext; 
    public CountryRepository(ProductsDbContext dbContext)
    {  
        _dbContext = dbContext;
    }

    public async Task<Country> Find(int id)
    {
        return await _dbContext.Countries.FirstAsync(x => x.Id == id);
    }

    public IQueryable<Country> Query(Expression<Func<Country, bool>>? expression = null)
    {
        return expression != null ?
            _dbContext.Countries.Where(expression) :
            _dbContext.Countries.AsQueryable();
    }

    public async Task Store(Country document)
    {
       await _dbContext.Countries.AddAsync(document);
    }

    public void Delete(Country document)
    {
        _dbContext.Countries.Remove(document);
    }
    
}