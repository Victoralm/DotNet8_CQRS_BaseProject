using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    [JsonIgnore]
    public virtual Category Category { get; set; }

}
