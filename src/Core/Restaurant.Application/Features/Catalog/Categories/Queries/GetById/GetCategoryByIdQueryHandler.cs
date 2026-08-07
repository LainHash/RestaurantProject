using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetById
{
    internal class GetCategoryByIdQueryHandler(ICategoryService categoryService)
                : IRequestHandler<GetCategoryByIdQuery, Result<CategoryResponse>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetCategoryByIdSpecification(request);
            var response = await _categoryService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
