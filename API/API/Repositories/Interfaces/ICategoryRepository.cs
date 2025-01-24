using API.Entities;

namespace API.Repositories.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(Guid Id);

}
