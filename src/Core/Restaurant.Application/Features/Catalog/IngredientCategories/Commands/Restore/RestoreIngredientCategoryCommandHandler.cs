using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Restore
{
    internal class RestoreIngredientCategoryCommandHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<RestoreIngredientCategoryCommand, Result>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result> Handle(RestoreIngredientCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreIngredientCategorySpecification(request);
            var response = await _categoryService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
