using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class ProductGetCommandHandler : IRequestHandler<ProductGetCommand, IEnumerable<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductGetCommandHandler> _logger;

    public ProductGetCommandHandler(IUnitOfWork unitOfWork, ILogger<ProductGetCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> Handle(ProductGetCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var products = await _unitOfWork.Products.GetProductsAsync();
            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(ProductGetCommandHandler));
            throw new ArgumentException("Invalid product data", nameof(request));
        }
    }
}
