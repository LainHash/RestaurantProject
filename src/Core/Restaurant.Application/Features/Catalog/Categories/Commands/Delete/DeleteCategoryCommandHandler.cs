using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Delete
{
    internal class DeleteCategoryCommandHandler(ICategoryService categoryService)
                : IRequestHandler<DeleteCategoryCommand, Result<object>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result<object>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteCategorySpecification(request);
            var response = await _categoryService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
