using AutoMapper;
using Restaurant.Application.Features.Catalog.Brands.Commands.Create;
using Restaurant.Application.Features.Catalog.Brands.Commands.Update;
using Restaurant.Application.Models.Messages;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    internal class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(
            IBrandRepository brandRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<BrandResponse>>> GetAllAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _brandRepository.CountAsync(specification, cancellationToken);

            var brands = await _brandRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<BrandResponse>>(brands);
            return PageResult<IEnumerable<BrandResponse>>
                .Succeed(response, Success<Brand>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<BrandResponse>> GetOneAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindAsync(specification, cancellationToken);
            if (brand == null)
            {
                return Result<BrandResponse>
                    .Fail(Error<Brand>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<BrandResponse>(brand);
            return Result<BrandResponse>
                .Succeed(response, Success<Brand>.Retrieved);
        }

        public async Task<Result<BrandResponse>> CreateAsync(
            CreateBrandCommand command,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if (brand is not null)
            {
                return Result<BrandResponse>
                    .Fail(Error<Brand>.ExistedName, HttpStatusCode.Conflict);
            }

            brand = _mapper.Map<Brand>(command.Body);
            _brandRepository.Add(brand);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<BrandResponse>(brand);
            return Result<BrandResponse>
                .Succeed(response, Success<Brand>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<BrandResponse>> UpdateAsync(
            UpdateBrandCommand command,
            UpdateBrandSpecification specification,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindAsync(specification, cancellationToken);
            if (brand is null)
            {
                return Result<BrandResponse>
                    .Fail(Error<Brand>.NotFound, HttpStatusCode.NotFound);
            }

            if (await _brandRepository.IsExistingNameAsync(command.Body.Name, cancellationToken))
            {
                return Result<BrandResponse>
                    .Fail(Error<Brand>.ExistedName, HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, brand);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<BrandResponse>(brand);
            return Result<BrandResponse>
                .Succeed(response, Success<Brand>.Updated, HttpStatusCode.OK);
        }

        public async Task<Result<object>> DeleteAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindAsync(specification, cancellationToken);
            if (brand == null)
            {
                return Result<object>
                    .Fail(Error<Brand>.NotFound, HttpStatusCode.NotFound);
            }

            if (brand.IsDeleted)
            {
                return Result<object>
                    .Fail(Error<Brand>.AlreadyDeleted, HttpStatusCode.BadRequest);
            }

            brand.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<object>
                .Succeed(default, Success<Brand>.Deleted);
        }

        public async Task<Result<object>> RestoreAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindAsync(specification, cancellationToken);
            if (brand == null)
            {
                return Result<object>
                    .Fail(Error<Brand>.NotFound, HttpStatusCode.NotFound);
            }

            if (!brand.IsDeleted)
            {
                return Result<object>
                    .Fail(Error<Brand>.NotYetDeleted, HttpStatusCode.BadRequest);
            }

            brand.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<object>
                .Succeed(default, Success<Brand>.Restored);
        }
    }
}
