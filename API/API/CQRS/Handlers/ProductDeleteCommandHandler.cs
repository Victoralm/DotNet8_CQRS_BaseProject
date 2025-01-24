using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, Product>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductDeleteCommandHandler> _logger;

    public ProductDeleteCommandHandler(IUnitOfWork unitOfWork, ILogger<ProductDeleteCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Product> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var product = await _unitOfWork.Products.GetProductByIdAsync(request.Id);

            if (product == null)
                return null;

            await _unitOfWork.Products.Delete(request.Id);
            await _unitOfWork.CompleteAsync();

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(ProductDeleteCommandHandler));
            throw new ArgumentException("Invalid product data", nameof(request));
        }
    }
}
