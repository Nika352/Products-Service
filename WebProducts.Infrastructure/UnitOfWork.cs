using WebProducts.Infrastructure.Persistence;
using WebProducts.Infrastructure.Persistence.Abstraction;

public interface IUnitOfWork
{
    Task<int> SaveAsync(CancellationToken cancellationToken);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductsDbContext _dbContext;

    public UnitOfWork(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        var entities = _dbContext.ChangeTracker.Entries<Entity>()
            .Select(x => x.Entity)
            .ToList();

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}