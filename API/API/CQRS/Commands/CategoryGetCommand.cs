using MediatR;
using API.Entities;

namespace API.CQRS.Commands;

public class CategoryGetCommand : IRequest<IEnumerable<Category>>
{
    public IEnumerable<Category> Categories { get; set; }
}
