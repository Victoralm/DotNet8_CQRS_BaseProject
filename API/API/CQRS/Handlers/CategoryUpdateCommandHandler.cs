using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, Category>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryUpdateCommandHandler> _logger;

    public CategoryUpdateCommandHandler(IUnitOfWork unitOfWork, ILogger<CategoryUpdateCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Category> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Category? category = await _unitOfWork.Category.GetCategoryByIdAsync(request.Id);

            if (category == null)
                return null;

            category.Name = request.Name;

            await _unitOfWork.Category.Upsert(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(CategoryUpdateCommandHandler));
            throw new ArgumentException("Invalid category data", nameof(request));
        }
    }
}
