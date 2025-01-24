using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, Product>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductAddCommandHandler> _logger;

    public ProductAddCommandHandler(IUnitOfWork unitOfWork, ILogger<ProductAddCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Product> Handle(ProductAddCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId
            };

            await _unitOfWork.Products.Add(product);
            await _unitOfWork.CompleteAsync();

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(ProductAddCommandHandler));
            throw new ArgumentException("Invalid product data", nameof(request));
        }
    }
}
