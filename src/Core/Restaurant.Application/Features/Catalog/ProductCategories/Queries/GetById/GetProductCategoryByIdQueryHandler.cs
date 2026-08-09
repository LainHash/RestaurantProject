using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetById
{
    internal class GetProductCategoryByIdQueryHandler(IProductCategoryService categoryService)
                : IRequestHandler<GetProductCategoryByIdQuery, Result<ProductCategoryResponse>>
    {
        private readonly IProductCategoryService _categoryService = categoryService;

        public async Task<Result<ProductCategoryResponse>> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetProductCategoryByIdSpecification(request);
            var response = await _categoryService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
