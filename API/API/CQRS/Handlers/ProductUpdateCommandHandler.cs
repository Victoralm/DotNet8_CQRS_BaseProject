using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductUpdateCommandHandler> _logger;

    public ProductUpdateCommandHandler(IUnitOfWork unitOfWork, ILogger<ProductUpdateCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Product? product = await _unitOfWork.Products.GetProductByIdAsync(request.Id);

            if (product == null)
                return null;

            product.Name = request.Name;

            await _unitOfWork.Products.Upsert(product);
            await _unitOfWork.CompleteAsync();

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(ProductUpdateCommandHandler));
            throw new ArgumentException("Invalid product data", nameof(request));
        }
    }
}
