using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Queries.GetByName
{
    internal class GetIngredientCategoryByNameQueryHandler(IIngredientCategoryService categoryService)
                : IRequestHandler<GetIngredientCategoryByNameQuery, Result<IngredientCategoryResponse>>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<Result<IngredientCategoryResponse>> Handle(GetIngredientCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetIngredientCategoryByNameSpecification(request);
            var response = await _categoryService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
