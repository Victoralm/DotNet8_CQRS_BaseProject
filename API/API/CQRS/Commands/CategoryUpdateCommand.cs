using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class CategoryUpdateCommand : IRequest<Category>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
