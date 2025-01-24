using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class CategoryDeleteCommand : IRequest<Category>
{
    public Guid Id { get; set; }
}
