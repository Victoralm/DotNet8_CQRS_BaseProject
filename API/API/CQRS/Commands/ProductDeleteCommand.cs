using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class ProductDeleteCommand : IRequest<Product>
{
    public Guid Id { get; set; }
}
