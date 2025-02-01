using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebProducts.Infrastructure.Persistence;
using WebProducts.Infrastructure.Repositories;

namespace WebProducts.Infrastructure;

public static class DIExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductsDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}