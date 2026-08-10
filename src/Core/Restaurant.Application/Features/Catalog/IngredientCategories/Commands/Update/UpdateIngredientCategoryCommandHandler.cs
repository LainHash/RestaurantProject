using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Update
{
    internal class UpdateIngredientCategoryCommandHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<UpdateIngredientCategoryCommand, Result<IngredientCategoryResponse>>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result<IngredientCategoryResponse>> Handle(UpdateIngredientCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateIngredientCategorySpecification(request);
            var response = await _categoryService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
