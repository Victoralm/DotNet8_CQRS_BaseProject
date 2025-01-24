using API.Context;
using API.Repositories.Implementations;
using API.UnitOfWork.Interfaces;

namespace API.UnitOfWork.Implementations;

public class UnitOfWork : IUnitOfWork
{

    private readonly PostgreContext _context;
    private readonly ILogger _logger;

    public ProductRepository Products { get; private set; }
    public CategoryRepository Category { get; private set; }

    public UnitOfWork(PostgreContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        Products = new ProductRepository(context, _logger);
        Category = new CategoryRepository(context, _logger);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}
