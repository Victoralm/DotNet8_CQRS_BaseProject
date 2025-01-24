using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}
