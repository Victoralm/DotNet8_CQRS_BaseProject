using MediatR;
using API.CQRS.Commands;
using API.Entities;
using API.UnitOfWork.Interfaces;

namespace API.CQRS.Handlers;

public class CategoryAddCommandHandler : IRequestHandler<CategoryAddCommand, Category>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryAddCommandHandler> _logger;

    public CategoryAddCommandHandler(IUnitOfWork unitOfWork, ILogger<CategoryAddCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Category> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await _unitOfWork.Category.Add(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Handle function error", typeof(CategoryAddCommandHandler));
            throw new ArgumentException("Invalid category data", nameof(request));
        }
    }
}
