using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class ProductGetByIdCommand : IRequest<Product>
{
    public Guid Id { get; set; }
}
