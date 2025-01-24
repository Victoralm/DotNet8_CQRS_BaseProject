using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class CategoryGetCommandHandler : IRequestHandler<CategoryGetCommand, IEnumerable<Category>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryGetCommandHandler> _logger;

    public CategoryGetCommandHandler(IUnitOfWork unitOfWork, ILogger<CategoryGetCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<Category>> Handle(CategoryGetCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var categories = await _unitOfWork.Category.GetCategoriesAsync();
            return categories;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(CategoryGetCommandHandler));
            throw new ArgumentException("Invalid category data", nameof(request));
        }
    }
}
