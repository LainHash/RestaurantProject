using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetAll
{
    internal class GetAllProductCategoriesQueryHandler(IProductCategoryService categoryService)
        : IRequestHandler<GetAllProductCategoriesQuery, PageResult<IEnumerable<ProductCategoryResponse>>>
    {
        private readonly IProductCategoryService _categoryService = categoryService;

        public async Task<PageResult<IEnumerable<ProductCategoryResponse>>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllProductCategoriesSpecification(request);
            var response = await _categoryService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
