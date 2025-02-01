using Microsoft.EntityFrameworkCore;
using WebProducts.Infrastructure.Persistence.Entities;

namespace WebProducts.Infrastructure.Persistence;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext()
    {
    }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.CategoryId);
        
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Country)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.CountryId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=db_WebProducts;Trusted_Connection=True;TrustServerCertificate=True");
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
}