using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Update
{
    internal class UpdateProductCategoryCommandHandler(IProductCategoryService categoryService)
                : IRequestHandler<UpdateProductCategoryCommand, Result<ProductCategoryResponse>>
    {
        private readonly IProductCategoryService _categoryService = categoryService;

        public async Task<Result<ProductCategoryResponse>> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateProductCategorySpecification(request);
            var response = await _categoryService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
