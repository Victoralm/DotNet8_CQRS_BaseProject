using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class ProductUpdateCommand : IRequest<Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}
