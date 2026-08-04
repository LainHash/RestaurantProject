using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Restore
{
    internal class RestoreBrandCommandHandler(IBrandService brandService)
                : IRequestHandler<RestoreBrandCommand, Result<object>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result<object>> Handle(RestoreBrandCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreBrandSpecification(request);
            var response = await _brandService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
