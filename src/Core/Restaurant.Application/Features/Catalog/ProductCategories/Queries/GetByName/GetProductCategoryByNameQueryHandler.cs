using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetByName
{
    internal class GetProductCategoryByNameQueryHandler(IProductCategoryService categoryService)
                : IRequestHandler<GetProductCategoryByNameQuery, Result<ProductCategoryResponse>>
    {
        private readonly IProductCategoryService _categoryService = categoryService;

        public async Task<Result<ProductCategoryResponse>> Handle(GetProductCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetProductCategoryByNameSpecification(request);
            var response = await _categoryService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
