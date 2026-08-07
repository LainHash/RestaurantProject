using Restaurant.Application.Features.Catalog.Products.Commands.Create;
using Restaurant.Application.Features.Catalog.Products.Commands.Update;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Catalog
{
    public interface IProductService
    {
        Task<PageResult<IEnumerable<ProductResponse>>> GetAllAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken);

        Task<Result<ProductResponse>> GetByIdAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken);

        Task<Result<ProductResponse>> CreateAsync(
            CreateProductSpecification specification,
            CreateProductRequest request,
            CancellationToken cancellationToken);

        Task<Result<ProductResponse>> UpdateAsync(
            UpdateProductSpecification specification,
            UpdateProductRequest request,
            CancellationToken cancellationToken);

        Task<Result<object>> DeleteAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken);

        Task<Result<object>> RestoreAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken);


    }
}
