using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Brands;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetAll
{
    internal class GetAllBrandsQueryHandler(IBrandService brandService)
        : IRequestHandler<GetAllBrandsQuery, PageResult<IEnumerable<BrandResponse>>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<PageResult<IEnumerable<BrandResponse>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllBrandsSpecification(request);
            var response = await _brandService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
