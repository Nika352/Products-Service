using System.Text.Json.Serialization;
using WebProducts.Infrastructure.Persistence.Abstraction;

namespace WebProducts.Infrastructure.Persistence.Entities;

public class Product : Entity
{
    public int Code {get; set;}
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int CountryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    
    public Category Category { get; set; }
    public Country Country { get; set; }
}