using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Queries.GetAll
{
    internal class GetAllIngredientCategoriesQueryHandler(IIngredientCategoryService categoryService)
        : IRequestHandler<GetAllIngredientCategoriesQuery, PageResult<IEnumerable<IngredientCategoryResponse>>>
    {
        private readonly IIngredientCategoryService _categoryService = categoryService;

        public async Task<PageResult<IEnumerable<IngredientCategoryResponse>>> Handle(GetAllIngredientCategoriesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllIngredientCategoriesSpecification(request);
            var response = await _categoryService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
