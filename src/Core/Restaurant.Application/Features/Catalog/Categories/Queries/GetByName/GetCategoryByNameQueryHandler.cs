using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetByName
{
    internal class GetCategoryByNameQueryHandler(ICategoryService categoryService)
                : IRequestHandler<GetCategoryByNameQuery, Result<CategoryResponse>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result<CategoryResponse>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetCategoryByNameSpecification(request);
            var response = await _categoryService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
