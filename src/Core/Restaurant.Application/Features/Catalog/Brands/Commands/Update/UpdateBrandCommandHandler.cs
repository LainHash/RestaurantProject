using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Update
{
    internal class UpdateBrandCommandHandler(IBrandService brandService)
                : IRequestHandler<UpdateBrandCommand, Result<BrandResponse>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result<BrandResponse>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateBrandSpecification(request);
            var response = await _brandService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
