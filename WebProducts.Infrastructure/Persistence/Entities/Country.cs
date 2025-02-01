using WebProducts.Infrastructure.Persistence.Abstraction;

namespace WebProducts.Infrastructure.Persistence.Entities;

public class Country : Entity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}