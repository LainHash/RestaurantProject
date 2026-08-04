using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll
{
    internal class GetAllCategoriesQueryHandler(ICategoryService categoryService)
        : IRequestHandler<GetAllCategoriesQuery, PageResult<IEnumerable<CategoryResponse>>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<PageResult<IEnumerable<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllCategoriesSpecification(request);
            var response = await _categoryService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
