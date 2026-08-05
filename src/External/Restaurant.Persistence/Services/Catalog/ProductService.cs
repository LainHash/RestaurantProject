using AutoMapper;
using Restaurant.Application.Models.Messages;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
    }
}
