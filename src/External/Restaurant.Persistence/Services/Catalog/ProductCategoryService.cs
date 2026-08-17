using AutoMapper;
using Restaurant.Application.Features.Catalog.ProductCategories.Commands.Create;
using Restaurant.Application.Features.Catalog.ProductCategories.Commands.Update;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    internal class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(
            IProductCategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<ProductCategoryResponse>>> GetAllAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _categoryRepository.CountAsync(specification, cancellationToken);

            var categories = await _categoryRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ProductCategoryResponse>>(categories);
            return PageResult<IEnumerable<ProductCategoryResponse>>
                .Succeed(response, Success<ProductCategory>.Retrieved, totalItems, specification.Skip, specification.Take);

        }

        public async Task<Result<ProductCategoryResponse>> GetOneAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result<ProductCategoryResponse>
                    .Fail(Error<ProductCategory>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ProductCategoryResponse>(category);
            return Result<ProductCategoryResponse>
                .Succeed(response, Success<ProductCategory>.Retrieved);
        }

        public async Task<Result<ProductCategoryResponse>> CreateAsync(
            CreateProductCategoryCommand command,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if(category is not null)
            {
                return Result<ProductCategoryResponse>
                    .Fail(Error<ProductCategory>.ExistedName, HttpStatusCode.Conflict);
            }

            category = _mapper.Map<ProductCategory>(command.Body);
            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ProductCategoryResponse>(category);
            return Result<ProductCategoryResponse>
                .Succeed(response, Success<ProductCategory>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<ProductCategoryResponse>> UpdateAsync(
            UpdateProductCategoryCommand command,
            UpdateProductCategorySpecification specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category is null)
            {
                return Result<ProductCategoryResponse>
                    .Fail(Error<ProductCategory>.NotFound, HttpStatusCode.NotFound);
            }

            if(await _categoryRepository.IsExistingNameAsync(command.Body.Name, cancellationToken))
            {
                return Result<ProductCategoryResponse>
                    .Fail(Error<ProductCategory>.ExistedName, HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ProductCategoryResponse>(category);
            return Result<ProductCategoryResponse>
                .Succeed(response, Success<ProductCategory>.Updated, HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Error<ProductCategory>.NotFound, HttpStatusCode.NotFound);
            }

            if(category.IsDeleted)
            {
                return Result
                    .Fail(Error<ProductCategory>.AlreadyDeleted, HttpStatusCode.BadRequest);
            }

            category.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success<ProductCategory>.Deleted);
        }

        public async Task<Result> RestoreAsync(
            ISpecification<ProductCategory> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Error<ProductCategory>.NotFound, HttpStatusCode.NotFound);
            }

            if(!category.IsDeleted)
            {
                return Result
                    .Fail(Error<ProductCategory>.NotYetDeleted, HttpStatusCode.BadRequest);
            }

            category.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success<ProductCategory>.Restored);
        }
    }
}
