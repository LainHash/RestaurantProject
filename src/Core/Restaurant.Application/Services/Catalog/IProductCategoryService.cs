using Restaurant.Application.Features.Catalog.ProductCategories.Commands.Create;
using Restaurant.Application.Features.Catalog.ProductCategories.Commands.Update;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Catalog
{
    public interface IProductCategoryService
    {
        Task<PageResult<IEnumerable<ProductCategoryResponse>>> GetAllAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken);

        Task<Result<ProductCategoryResponse>> GetOneAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken);

        Task<Result<ProductCategoryResponse>> CreateAsync(
            CreateProductCategoryCommand command,
            CancellationToken cancellationToken);

        Task<Result<ProductCategoryResponse>> UpdateAsync(
            UpdateProductCategoryCommand command,
            UpdateProductCategorySpecification specification,
            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken);

        Task<Result> RestoreAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken);
    }
}
