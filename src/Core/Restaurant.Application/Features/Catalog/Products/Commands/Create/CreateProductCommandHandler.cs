using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    internal class CreateProductCommandHandler(IProductService productService)
                : IRequestHandler<CreateProductCommand, Result<ProductResponse>>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateProductSpecification(request);
            var response = await _productService.CreateAsync(specification, request.Body, cancellationToken);
            return response;
        }
    }
}
