using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetByName
{
    internal class GetBrandByNameQueryHandler(IBrandService brandService)
                : IRequestHandler<GetBrandByNameQuery, Result<BrandResponse>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result<BrandResponse>> Handle(GetBrandByNameQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetBrandByNameSpecification(request);
            var response = await _brandService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
