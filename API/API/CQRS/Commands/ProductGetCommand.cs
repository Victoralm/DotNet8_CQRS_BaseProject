using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class ProductGetCommand : IRequest<IEnumerable<Product>>
{
    public IEnumerable<Product> Products { get; set; }
}
