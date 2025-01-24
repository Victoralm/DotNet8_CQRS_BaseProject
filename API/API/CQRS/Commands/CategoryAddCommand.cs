using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class CategoryAddCommand : IRequest<Category>
{
    public string Name { get; set; }
}
