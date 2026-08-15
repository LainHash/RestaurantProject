using MediatR;
using Restaurant.Application.Features.Catalog.Products.Commands.Restore;
using Restaurant.Application.Services.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Delete
{
    internal class DeleteProductCommandHandler(IProductService productService)
                : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteProductSpecification(request);
            var response = await _productService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
