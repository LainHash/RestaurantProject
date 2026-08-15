using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Restore
{
    internal class RestoreProductCommandHandler(IProductService productService)
                : IRequestHandler<RestoreProductCommand, Result>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result> Handle(RestoreProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreProductSpecification(request);
            var response = await _productService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
