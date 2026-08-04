using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Brands;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetById
{
    internal class GetBrandByIdQueryHandler(IBrandService brandService)
                : IRequestHandler<GetBrandByIdQuery, Result<BrandResponse>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result<BrandResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetBrandByIdSpecification(request);
            var response = await _brandService.GetOneAsync(specification, cancellationToken);
            return response;
        }
    }
}
