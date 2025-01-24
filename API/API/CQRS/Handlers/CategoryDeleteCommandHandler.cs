using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class CategoryDeleteCommandHandler : IRequestHandler<CategoryDeleteCommand, Category>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryDeleteCommandHandler> _logger;

    public CategoryDeleteCommandHandler(IUnitOfWork unitOfWork, ILogger<CategoryDeleteCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Category> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var category = await _unitOfWork.Category.GetCategoryByIdAsync(request.Id);

            if (category == null)
                return null;

            await _unitOfWork.Category.Delete(request.Id);
            await _unitOfWork.CompleteAsync();

            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(CategoryDeleteCommandHandler));
            throw new ArgumentException("Invalid category data", nameof(request));
        }
    }
}
