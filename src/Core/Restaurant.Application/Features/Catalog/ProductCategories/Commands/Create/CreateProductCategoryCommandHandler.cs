using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Create
{
    internal class CreateProductCategoryCommandHandler(IProductCategoryService categoryService)
                : IRequestHandler<CreateProductCategoryCommand, Result<ProductCategoryResponse>>
    {
        private readonly IProductCategoryService _categoryService = categoryService;

        public async Task<Result<ProductCategoryResponse>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _categoryService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}
