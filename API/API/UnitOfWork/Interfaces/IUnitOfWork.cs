using API.Repositories.Implementations;

namespace API.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    ////Define the Specific Repositories
    ProductRepository Products { get; }
    CategoryRepository Category { get; }

    Task CompleteAsync();
}
