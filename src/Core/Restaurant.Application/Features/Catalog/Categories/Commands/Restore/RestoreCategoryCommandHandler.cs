using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Restore
{
    internal class RestoreCategoryCommandHandler(ICategoryService categoryService)
                : IRequestHandler<RestoreCategoryCommand, Result<object>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result<object>> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreCategorySpecification(request);
            var response = await _categoryService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
