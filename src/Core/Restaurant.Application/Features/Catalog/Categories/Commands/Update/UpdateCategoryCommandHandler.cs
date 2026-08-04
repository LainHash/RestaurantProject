using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Update
{
    internal class UpdateCategoryCommandHandler(ICategoryService categoryService)
                : IRequestHandler<UpdateCategoryCommand, Result<CategoryResponse>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateCategorySpecification(request);
            var response = await _categoryService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
