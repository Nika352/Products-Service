using System.Linq.Expressions;
using WebProducts.Infrastructure.Persistence.Entities;

namespace WebProducts.Infrastructure.Repositories.Repository;

public interface IRepository<T>  where T : class
{
    Task<T> Find(int id);

    IQueryable<T> Query(Expression<Func<T, bool>>? expression = null);

    Task Store(T document);

    void Delete(int id);
}