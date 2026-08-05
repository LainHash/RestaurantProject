using AutoMapper;
using Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByProductId;
using Restaurant.Application.Models.Messages;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Inventory;
using System.Net;

namespace Restaurant.Persistence.Services.Inventory
{
    internal class ProductStockService : IProductStockService
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductStockService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IProductStockRepository productStockRepository,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productStockRepository = productStockRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<IEnumerable<ProductStockResponse>>> GetAllByProductIdAsync(
            GetAllProductStocksByProductIdQuery query,
            GetAllProductStocksByProductIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(query.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<IEnumerable<ProductStockResponse>>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if(product.InventoryType == InventoryType.MadeToOrder)
            {
                return Result<IEnumerable<ProductStockResponse>>
                    .Fail("This Product is made to order.");
            }

            var productStocks = await _productStockRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ProductStockResponse>>(productStocks);
            return Result<IEnumerable<ProductStockResponse>>
                .Succeed(response, Success<ProductStock>.Retrieved);
        }
    }
}
