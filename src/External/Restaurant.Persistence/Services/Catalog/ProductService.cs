using AutoMapper;
using CloudinaryDotNet.Core;
using Restaurant.Application.Features.Catalog.Products.Commands.Create;
using Restaurant.Application.Features.Catalog.Products.Commands.Update;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitRepository _unitRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IProductCategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IUnitRepository unitRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _unitRepository = unitRepository;
        }

        public async Task<PageResult<IEnumerable<ProductResponse>>> GetAllAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _productRepository.CountAsync(specification, cancellationToken);

            var products = await _productRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return PageResult<IEnumerable<ProductResponse>>
                .Succeed(response, Success<Product>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<ProductResponse>> GetByIdAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(specification, cancellationToken);
            if(product is null)
            {
                return Result<ProductResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ProductResponse>(product);
            return Result<ProductResponse>
                .Succeed(response, Success<Product>.Retrieved);
        }

        public async Task<Result<ProductResponse>> CreateAsync(
            CreateProductSpecification specification,
            CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByIdAsync(request.CategoryId, cancellationToken);
            if(category is null)
            {
                return Result<ProductResponse>
                    .Fail(Error<ProductCategory>.NotFound, HttpStatusCode.NotFound);
            }

            Brand? brand = null;
            if (!string.IsNullOrEmpty(request.BrandId))
            {
                brand = await _brandRepository.FindByIdAsync(request.BrandId, cancellationToken);

                if (brand is null)
                {
                    return Result<ProductResponse>
                        .Fail(Error<Brand>.NotFound, HttpStatusCode.NotFound);
                }
            }

            var unit = await _unitRepository.FindByIdAsync(request.UnitId, cancellationToken);
            if (unit is null)
            {
                return Result<ProductResponse>
                    .Fail(Error<Unit>.NotFound, HttpStatusCode.NotFound);
            }

            var product = _mapper.Map<Product>(request)
                .SetCategory(category.Id)
                .SetBrand(brand?.Id)
                .SetUnit(unit.Id);

            _productRepository.Add(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            specification.ApplyCriteria(product.Id);
            var createdProduct = await _productRepository.FindAsync(specification, cancellationToken);
            
            var response = _mapper.Map<ProductResponse>(createdProduct);
            return Result<ProductResponse>
                .Succeed(response, Success<Product>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<ProductResponse>> UpdateAsync(
            UpdateProductSpecification specification,
            UpdateProductRequest request,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByIdAsync(request.CategoryId, cancellationToken);
            if (category is null)
            {
                return Result<ProductResponse>
                    .Fail(Error<ProductCategory>.NotFound, HttpStatusCode.NotFound);
            }

            Brand? brand = null;
            if (!string.IsNullOrEmpty(request.BrandId))
            {
                brand = await _brandRepository.FindByIdAsync(request.BrandId, cancellationToken);

                if (brand is null)
                {
                    return Result<ProductResponse>
                        .Fail(Error<Brand>.NotFound, HttpStatusCode.NotFound);
                }
            }

            var unit = await _unitRepository.FindByIdAsync(request.UnitId, cancellationToken);
            if (unit is null)
            {
                return Result<ProductResponse>
                    .Fail(Error<Unit>.NotFound, HttpStatusCode.NotFound);
            }

            var product = await _productRepository.FindAsync(specification, cancellationToken);
            if(product is null)
            {
                return Result<ProductResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            _mapper.Map(request, product)
                .SetCategory(category.Id)
                .SetBrand(brand?.Id)
                .SetUnit(unit.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedProduct = await _productRepository.FindAsync(specification, cancellationToken);

            var response = _mapper.Map<ProductResponse>(updatedProduct);
            return Result<ProductResponse>
                .Succeed(response, Success<Product>.Updated);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(specification, cancellationToken);
            if (product is null)
            {
                return Result
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if (product.IsDeleted)
            {
                return Result
                    .Fail(Error<Product>.AlreadyDeleted, HttpStatusCode.BadRequest);
            }

            product.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success<Product>.Deleted);
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(specification, cancellationToken);
            if (product is null)
            {
                return Result
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if (!product.IsDeleted)
            {
                return Result
                    .Fail(Error<Product>.NotYetDeleted, HttpStatusCode.BadRequest);
            }

            product.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success<Product>.Restored);
        }
    }
}
