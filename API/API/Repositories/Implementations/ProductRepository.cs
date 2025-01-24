
using API.Context;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace API.Repositories.Implementations;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(PostgreContext context, ILogger logger) : base(context, logger) { }

    public async Task<Product?> GetProductByIdAsync(Guid Id)
    {
        using var connection = _context.DapperConnection();
        #region Dapper
        try
        {
            var sql = $@"
                SELECT * FROM dev.""Products"" WHERE ""Id"" = @Id
                ";

            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = Id });
        }
        #endregion
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} GetCategoryByIdAsync function error", typeof(CategoryRepository));
            return new Product();
        }
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetAllUsersAsync function error", typeof(ProductRepository));
            return new List<Product>();
        }
    }

    public override async Task<bool> Upsert(Product entity)
    {
        try
        {
            var existingProduct = await _dbSet.Where(x => x.Id == entity.Id)
                                                .FirstOrDefaultAsync();

            if (existingProduct == null)
                return await Add(entity);

            existingProduct.Name = entity.Name;
            existingProduct.Description = entity.Description;
            existingProduct.CategoryId = entity.CategoryId;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Upsert function error", typeof(ProductRepository));
            return false;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var exist = await _dbSet.Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();

            if (exist == null) return false;

            _dbSet.Remove(exist);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Delete function error", typeof(ProductRepository));
            return false;
        }
    }
}
