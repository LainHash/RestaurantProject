using AutoMapper;
using Restaurant.Application.Features.Inventory.IngredientStocks.Commands.UpdateQuantity;
using Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByBranchId;
using Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByIngredientId;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Inventory;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Domain.Repositories.Territory;
using System.Net;

namespace Restaurant.Persistence.Services.Inventory
{
    internal class IngredientStockService : IIngredientStockService
    {
        private readonly IIngredientStockRepository _ingredientStockRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IBranchRepository _branchRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public IngredientStockService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IIngredientStockRepository ingredientStockRepository,
            IIngredientRepository ingredientRepository,
            IBranchRepository branchRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ingredientStockRepository = ingredientStockRepository;
            _ingredientRepository = ingredientRepository;
            _branchRepository = branchRepository;
        }

        public async Task<Result<IEnumerable<IngredientStockResponse>>> GetAllByIngredientIdAsync(
            GetAllIngredientStocksByIngredientIdQuery query,
            GetAllIngredientStocksByIngredientIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.FindByIdAsync(query.IngredientId, cancellationToken);
            if (ingredient is null)
            {
                return Result<IEnumerable<IngredientStockResponse>>
                    .Fail(Error<Ingredient>.NotFound, HttpStatusCode.NotFound);
            }

            var ingredientStocks = await _ingredientStockRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<IngredientStockResponse>>(ingredientStocks);
            return Result<IEnumerable<IngredientStockResponse>>
                .Succeed(response, Success<IngredientStock>.Retrieved);
        }

        public async Task<Result<IEnumerable<IngredientStockResponse>>> GetAllByBranchIdAsync(
            GetAllIngredientStockByBranchIdQuery query,
            GetAllIngredientStockByBranchIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FindByIdAsync(query.BranchId, cancellationToken);
            if (branch is null)
            {
                return Result<IEnumerable<IngredientStockResponse>>
                    .Fail(Error<Branch>.NotFound, HttpStatusCode.NotFound);
            }

            var ingredientStocks = await _ingredientStockRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<IngredientStockResponse>>(ingredientStocks);
            return Result<IEnumerable<IngredientStockResponse>>
                .Succeed(response, Success<IngredientStock>.Retrieved);
        }

        public async Task<Result<IngredientStockResponse>> UpdateQuantityAsync(
            UpdateIngredientStockQuantityCommand command,
            UpdateIngredientStockQuantitySpecification specification,
            CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.FindByIdAsync(command.IngredientId, cancellationToken);
            if (ingredient is null)
            {
                return Result<IngredientStockResponse>
                    .Fail(Error<Ingredient>.NotFound, HttpStatusCode.NotFound);
            }

            var branch = await _branchRepository.FindByIdAsync(command.BranchId, cancellationToken);
            if (branch is null)
            {
                return Result<IngredientStockResponse>
                    .Fail(Error<Branch>.NotFound, HttpStatusCode.NotFound);
            }

            var ingredientStock = await _ingredientStockRepository.FindAsync(specification, cancellationToken);
            if (ingredientStock is null)
            {
                return Result<IngredientStockResponse>
                    .Fail(Error<IngredientStock>.NotFound, HttpStatusCode.NotFound);
            }

            if (ingredientStock.QuantityOnHand - command.Body.Amount < 0)
            {
                return Result<IngredientStockResponse>
                    .Fail("Quantity on hand can not be negative.");
            }

            ingredientStock.UpdateQuantity(command.Body.Amount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<IngredientStockResponse>(ingredientStock);
            return Result<IngredientStockResponse>
                .Succeed(response, Success<IngredientStock>.Updated);
        }
    }
}
