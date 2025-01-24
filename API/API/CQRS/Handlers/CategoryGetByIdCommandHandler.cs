using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class CategoryGetByIdCommandHandler : IRequestHandler<CategoryGetByIdCommand, Category>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryGetByIdCommandHandler> _logger;

    public CategoryGetByIdCommandHandler(IUnitOfWork unitOfWork, ILogger<CategoryGetByIdCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Category> Handle(CategoryGetByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var category = await _unitOfWork.Category.GetCategoryByIdAsync(request.Id);

            if (category == null)
                return null;

            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(CategoryGetByIdCommandHandler));
            throw new ArgumentException("Invalid category data", nameof(request));
        }
    }
}
