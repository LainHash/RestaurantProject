using AutoMapper;
using Restaurant.Application.Features.Production.Recipes.Commands.AddIngredient;
using Restaurant.Application.Features.Production.Recipes.Commands.Create;
using Restaurant.Application.Features.Production.Recipes.Commands.Update;
using Restaurant.Application.Features.Production.Recipes.Queries.GetAll;
using Restaurant.Application.Features.Production.Recipes.Queries.GetAllByProductId;
using Restaurant.Application.Features.Production.Recipes.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Production;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Domain.Repositories.Production;
using System.Net;
using System.Xml;

namespace Restaurant.Persistence.Services.Production
{
    internal class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(
            IRecipeRepository recipeRepository,
            IMapper mapper,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IIngredientRepository ingredientRepository,
            IUnitRepository unitRepository,
            IRecipeIngredientRepository recipeIngredientRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _ingredientRepository = ingredientRepository;
            _unitRepository = unitRepository;
            _recipeIngredientRepository = recipeIngredientRepository;
        }

        public async Task<Result<IEnumerable<RecipeResponse>>> GetAllAsync(
            GetAllRecipesSpecification specification,
            CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<RecipeResponse>>(recipes);
            return Result<IEnumerable<RecipeResponse>>
                .Succeed(response, Success<Recipe>.Retrieved);
        }

        public async Task<Result<RecipeResponse>> GetByIdAsync(
            GetRecipeByIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.FindAsync(specification, cancellationToken);
            if (recipe is null)
            {
                return Result<RecipeResponse>
                    .Fail(Error<Recipe>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<RecipeResponse>(recipe);
            return Result<RecipeResponse>
                .Succeed(response, Success<Recipe>.Retrieved);
        }

        public async Task<Result<IEnumerable<RecipeResponse>>> GetAllByProductIdAsync(
            GetAllRecipesByProductIdQuery query,
            GetAllRecipesByProductIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(query.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<IEnumerable<RecipeResponse>>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if (product.InventoryType == InventoryType.StockTracked)
            {
                return Result<IEnumerable<RecipeResponse>>
                    .Fail("Cannot retrieve recipes because this product is stock-tracked.", HttpStatusCode.NotFound);
            }

            var recipes = await _recipeRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<RecipeResponse>>(recipes);
            return Result<IEnumerable<RecipeResponse>>
                .Succeed(response, Success<Recipe>.Retrieved);
        }

        public async Task<Result<RecipeResponse>> CreateAsync(
            CreateRecipeCommand command,
            CreateRecipeSpecification specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<RecipeResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if (product.InventoryType == InventoryType.StockTracked)
            {
                return Result<RecipeResponse>
                    .Fail("Cannot create recipes because this product is stock-tracked.", HttpStatusCode.NotFound);
            }

            var recipe = _mapper.Map<Recipe>(command.Body);
            recipe.SetProduct(product.Id);
            _recipeRepository.Add(recipe);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            specification.ApplyCriteria(recipe.Id);
            var createdRecipe = await _recipeRepository.FindAsync(specification, cancellationToken);

            var response = _mapper.Map<RecipeResponse>(createdRecipe);
            return Result<RecipeResponse>
                .Succeed(response, Success<Recipe>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<RecipeResponse>> UpdateAsync(
            UpdateRecipeCommand command,
            UpdateRecipeSpecification specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<RecipeResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if (product.InventoryType == InventoryType.StockTracked)
            {
                return Result<RecipeResponse>
                    .Fail("Cannot create recipes because this product is stock-tracked.", HttpStatusCode.NotFound);
            }

            var recipe = await _recipeRepository.FindAsync(specification, cancellationToken);
            if (recipe is null)
            {
                return Result<RecipeResponse>
                    .Fail(Error<Recipe>.NotFound, HttpStatusCode.NotFound);
            }

            _mapper.Map(command.Body, recipe);
            recipe.SetProduct(product.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<RecipeResponse>(recipe);
            return Result<RecipeResponse>
                .Succeed(response, Success<Recipe>.Updated);
        }

        public async Task<Result<RecipeResponse>> AddIngredientAsync(
            AddRecipeIngredientCommand command,
            AddRecipeIngredientSpecification specification,
            CancellationToken cancellationToken)
        {

            var recipe = await _recipeRepository.FindAsync(specification, cancellationToken);
            if (recipe is null)
            {
                return Result<RecipeResponse>
                    .Fail(Error<Recipe>.NotFound, HttpStatusCode.NotFound);
            }

            foreach (var item in command.Body)
            {
                var ingredient = await _ingredientRepository.FindByIdAsync(item.IngredientId, cancellationToken);
                if (ingredient is null)
                {
                    return Result<RecipeResponse>
                        .Fail(Error<Ingredient>.NotFound, HttpStatusCode.NotFound);
                }

                var unit = await _unitRepository.FindByIdAsync(item.UnitId, cancellationToken);
                if (unit is null)
                {
                    return Result<RecipeResponse>
                        .Fail(Error<Unit>.NotFound, HttpStatusCode.NotFound);
                }

                var alreadyExists = recipe.RecipeIngredients.Any(ri => ri.IngredientId == ingredient.Id);
                if (!alreadyExists)
                {
                    var recipeIngredient = _mapper.Map<RecipeIngredient>(item)
                        .SetRecipe(recipe.Id)
                        .SetIngredient(ingredient.Id)
                        .SetUnit(unit.Id);

                    _recipeIngredientRepository.Add(recipeIngredient);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedRecipe = await _recipeRepository.FindAsync(specification, cancellationToken);

            var response = _mapper.Map<RecipeResponse>(updatedRecipe);
            return Result<RecipeResponse>.Succeed(response, Success<Recipe>.Updated);
        }
    }
}
