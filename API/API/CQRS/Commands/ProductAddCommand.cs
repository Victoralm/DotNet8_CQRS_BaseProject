using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class ProductAddCommand : IRequest<Product>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}
