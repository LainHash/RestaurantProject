using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Restore
{
    internal class RestoreProductCategoryCommandHandler(IProductCategoryService categoryService)
                : IRequestHandler<RestoreProductCategoryCommand, Result>
    {
        private readonly IProductCategoryService _categoryService = categoryService;

        public async Task<Result> Handle(RestoreProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreProductCategorySpecification(request);
            var response = await _categoryService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
