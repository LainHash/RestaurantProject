using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Delete
{
    internal class DeleteIngredientCategoryCommandHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<DeleteIngredientCategoryCommand, Result<object>>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result<object>> Handle(DeleteIngredientCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteIngredientCategorySpecification(request);
            var response = await _categoryService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
