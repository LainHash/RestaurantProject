using Restaurant.Application.Features.Catalog.Brands.Commands.Create;
using Restaurant.Application.Features.Catalog.Brands.Commands.Update;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Catalog
{
    public interface IBrandService
    {
        Task<PageResult<IEnumerable<BrandResponse>>> GetAllAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken);

        Task<Result<BrandResponse>> GetOneAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken);

        Task<Result<BrandResponse>> CreateAsync(
            CreateBrandCommand command,
            CancellationToken cancellationToken);

        Task<Result<BrandResponse>> UpdateAsync(
            UpdateBrandCommand command,
            UpdateBrandSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<object>> DeleteAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken);

        Task<Result<object>> RestoreAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken);
    }
}
