using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    internal class UpdateProductCommandHandler(IProductService productService)
                : IRequestHandler<UpdateProductCommand, Result<ProductResponse>>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateProductSpecification(request);
            var response = await _productService.UpdateAsync(specification, request.Body, cancellationToken);
            return response;
        }
    }
}
