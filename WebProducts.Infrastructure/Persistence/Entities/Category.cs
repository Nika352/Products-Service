using WebProducts.Infrastructure.Persistence.Abstraction;

namespace WebProducts.Infrastructure.Persistence.Entities;

public class Category : Entity
{
    public string Name { get; set; }

    public int ParentId { get; set; }
    
    public List<Product> Products { get; set; }
}