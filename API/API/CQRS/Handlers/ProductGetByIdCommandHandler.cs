using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class ProductGetByIdCommandHandler : IRequestHandler<ProductGetByIdCommand, Product>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductGetByIdCommandHandler> _logger;

    public ProductGetByIdCommandHandler(IUnitOfWork unitOfWork, ILogger<ProductGetByIdCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Product> Handle(ProductGetByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var product = await _unitOfWork.Products.GetProductByIdAsync(request.Id);

            if (product == null)
                return null;

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(ProductGetByIdCommandHandler));
            throw new ArgumentException("Invalid product data", nameof(request));
        }
    }
}
