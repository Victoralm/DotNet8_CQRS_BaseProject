using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class CategoryGetByIdCommand : IRequest<Category>
{
    public Guid Id { get; set; }
}
