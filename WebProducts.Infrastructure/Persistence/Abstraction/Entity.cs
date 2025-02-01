using System.ComponentModel.DataAnnotations;

namespace WebProducts.Infrastructure.Persistence.Abstraction;

public class Entity
{
    [Key]
    public int Id { get; set; }
}