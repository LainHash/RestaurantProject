using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Delete
{
    internal class DeleteProductCategoryCommandHandler(IProductCategoryService categoryService)
                : IRequestHandler<DeleteProductCategoryCommand, Result>
    {
        private readonly IProductCategoryService _categoryService = categoryService;

        public async Task<Result> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteProductCategorySpecification(request);
            var response = await _categoryService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
