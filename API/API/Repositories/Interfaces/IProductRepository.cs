using API.Entities;

namespace API.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid Id);

}
