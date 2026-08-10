using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Create
{
    internal class CreateIngredientCategoryCommandHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<CreateIngredientCategoryCommand, Result<IngredientCategoryResponse>>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result<IngredientCategoryResponse>> Handle(CreateIngredientCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _categoryService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}
