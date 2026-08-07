using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetById
{
    internal class GetProductByIdQueryHandler(IProductService productService)
        : IRequestHandler<GetProductByIdQuery, Result<ProductResponse>>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetProductByIdSpecification(request);
            var response = await _productService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
