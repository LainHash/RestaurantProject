using AutoMapper;
using Restaurant.Application.Features.Inventory.ProductStocks.Commands.UpdateQuantity;
using Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByBranchId;
using Restaurant.Application.Features.Inventory.ProductStocks.Queries.GetAllByProductId;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Persistence.Services.Inventory
{
    internal class ProductStockService : IProductStockService
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBranchRepository _branchRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductStockService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IProductStockRepository productStockRepository,
            IProductRepository productRepository,
            IBranchRepository branchRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productStockRepository = productStockRepository;
            _productRepository = productRepository;
            _branchRepository = branchRepository;
        }

        public async Task<Result<IEnumerable<ProductStockResponse>>> GetAllByProductIdAsync(
            GetAllProductStocksByProductIdQuery query,
            GetAllProductStocksByProductIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(query.ProductId, cancellationToken);
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

        public async Task<Result<IEnumerable<ProductStockResponse>>> GetAllByBranchIdAsync(
            GetAllProductStockByBranchIdQuery query,
            GetAllProductStockByBranchIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FindByIdAsync(query.BranchId, cancellationToken);
            if(branch is null)
            {
                return Result<IEnumerable<ProductStockResponse>>
                    .Fail(Error<Branch>.NotFound, HttpStatusCode.NotFound);
            }

            var productStocks = await _productStockRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ProductStockResponse>>(productStocks);
            return Result<IEnumerable<ProductStockResponse>>
                .Succeed(response, Success<ProductStock>.Retrieved);
        }

        public async Task<Result<ProductStockResponse>> UpdateQuantityAsync(
            UpdateProductStockQuantityCommand command,
            UpdateProductStockQuantitySpecification specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(command.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<ProductStockResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if (product.InventoryType == InventoryType.MadeToOrder)
            {
                return Result<ProductStockResponse>
                    .Fail("This Product is made to order.");
            }

            var branch = await _branchRepository.FindByIdAsync(command.BranchId, cancellationToken);
            if (branch is null)
            {
                return Result<ProductStockResponse>
                    .Fail(Error<Branch>.NotFound, HttpStatusCode.NotFound);
            }

            var productStock = await GetOrCreateAsync(
                specification,
                () => new ProductStock()
                    .SetProduct(product.Id)
                    .SetBranch(branch.Id),
                cancellationToken);

            if(productStock.QuantityOnHand - command.Body.Amount < 0)
            {
                return Result<ProductStockResponse>
                    .Fail("Quantity on hand can not be negative.");
            }

            productStock.UpdateQuantity(command.Body.Amount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ProductStockResponse>(productStock);
            return Result<ProductStockResponse>
                .Succeed(response, Success<ProductStock>.Updated);
        }

        private async Task<ProductStock> InitializeAsync(
            Func<ProductStock> factory,
            CancellationToken cancellationToken)
        {
            var productStock = factory();

            _productStockRepository.Add(productStock);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return productStock;
        }

        private async Task<ProductStock> GetOrCreateAsync(
            ISpecification<ProductStock> specification,
            Func<ProductStock> factory,
            CancellationToken cancellationToken)
        {
            var productStock = await _productStockRepository.FindAsync(specification, cancellationToken);

            if (productStock is not null)
                return productStock;

            return await InitializeAsync(factory, cancellationToken);
        }
    }
}
