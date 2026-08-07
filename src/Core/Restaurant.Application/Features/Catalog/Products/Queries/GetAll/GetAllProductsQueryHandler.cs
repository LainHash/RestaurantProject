using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    internal class GetAllProductsQueryHandler(IProductService productService)
                : IRequestHandler<GetAllProductsQuery, PageResult<IEnumerable<ProductResponse>>>
    {
        private readonly IProductService _productService = productService;

        public async Task<PageResult<IEnumerable<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllProductsSpecification(request);
            var response = await _productService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
