using AutoMapper;
using Restaurant.Application.Features.Catalog.Ingredients.Commands.Create;
using Restaurant.Application.Features.Catalog.Ingredients.Commands.Update;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Specifications;
using Restaurant.Domain.Entities.Inventory;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    internal class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IIngredientCategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitRepository _unitRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public IngredientService(
            IIngredientRepository ingredientRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IIngredientCategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IUnitRepository unitRepository)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _unitRepository = unitRepository;
        }

        public async Task<PageResult<IEnumerable<IngredientResponse>>> GetAllAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _ingredientRepository.CountAsync(specification, cancellationToken);

            var ingredients = await _ingredientRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<IngredientResponse>>(ingredients);
            return PageResult<IEnumerable<IngredientResponse>>
                .Succeed(response, Success<Ingredient>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<IngredientResponse>> GetByIdAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.FindAsync(specification, cancellationToken);
            if (ingredient is null)
            {
                return Result<IngredientResponse>
                    .Fail(Error<Ingredient>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<IngredientResponse>(ingredient);
            return Result<IngredientResponse>
                .Succeed(response, Success<Ingredient>.Retrieved);
        }

        public async Task<Result<IngredientResponse>> CreateAsync(
            CreateIngredientSpecification specification,
            CreateIngredientRequest request,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByIdAsync(request.CategoryId, cancellationToken);
            if (category is null)
            {
                return Result<IngredientResponse>
                    .Fail(Error<IngredientCategory>.NotFound, HttpStatusCode.NotFound);
            }

            Brand? brand = null;
            if (!string.IsNullOrEmpty(request.BrandId))
            {
                brand = await _brandRepository.FindByIdAsync(request.BrandId, cancellationToken);

                if (brand is null)
                {
                    return Result<IngredientResponse>
                        .Fail(Error<Brand>.NotFound, HttpStatusCode.NotFound);
                }
            }

            var unit = await _unitRepository.FindByIdAsync(request.UnitId, cancellationToken);
            if (unit is null)
            {
                return Result<IngredientResponse>
                    .Fail(Error<Unit>.NotFound, HttpStatusCode.NotFound);
            }

            var ingredient = _mapper.Map<Ingredient>(request)
                .SetCategory(category.Id)
                .SetBrand(brand?.Id)
                .SetUnit(unit.Id);
            _ingredientRepository.Add(ingredient);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            specification.ApplyCriteria(ingredient.Id);
            var createdIngredient = await _ingredientRepository.FindAsync(specification, cancellationToken);

            var response = _mapper.Map<IngredientResponse>(createdIngredient);
            return Result<IngredientResponse>
                .Succeed(response, Success<Ingredient>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<IngredientResponse>> UpdateAsync(
            UpdateIngredientSpecification specification,
            UpdateIngredientRequest request,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByIdAsync(request.CategoryId, cancellationToken);
            if (category is null)
            {
                return Result<IngredientResponse>
                    .Fail(Error<IngredientCategory>.NotFound, HttpStatusCode.NotFound);
            }

            Brand? brand = null;
            if (!string.IsNullOrEmpty(request.BrandId))
            {
                brand = await _brandRepository.FindByIdAsync(request.BrandId, cancellationToken);

                if (brand is null)
                {
                    return Result<IngredientResponse>
                        .Fail(Error<Brand>.NotFound, HttpStatusCode.NotFound);
                }
            }

            var unit = await _unitRepository.FindByIdAsync(request.UnitId, cancellationToken);
            if (unit is null)
            {
                return Result<IngredientResponse>
                    .Fail(Error<Unit>.NotFound, HttpStatusCode.NotFound);
            }

            var ingredient = await _ingredientRepository.FindAsync(specification, cancellationToken);
            if (ingredient is null)
            {
                return Result<IngredientResponse>
                    .Fail(Error<Ingredient>.NotFound, HttpStatusCode.NotFound);
            }

            _mapper.Map(request, ingredient)
                .SetCategory(category.Id)
                .SetBrand(brand?.Id)
                .SetUnit(unit.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedIngredient = await _ingredientRepository.FindAsync(specification, cancellationToken);

            var response = _mapper.Map<IngredientResponse>(updatedIngredient);
            return Result<IngredientResponse>
                .Succeed(response, Success<Ingredient>.Updated);
        }

        public async Task<Result<object>> DeleteAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.FindAsync(specification, cancellationToken);
            if (ingredient is null)
            {
                return Result<object>
                    .Fail(Error<Ingredient>.NotFound, HttpStatusCode.NotFound);
            }

            if (ingredient.IsDeleted)
            {
                return Result<object>
                    .Fail(Error<Ingredient>.AlreadyDeleted, HttpStatusCode.BadRequest);
            }

            ingredient.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<object>
                .Succeed(default, Success<Ingredient>.Deleted);
        }

        public async Task<Result<object>> RestoreAsync(
            ISpecification<Ingredient> specification,
            CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.FindAsync(specification, cancellationToken);
            if (ingredient is null)
            {
                return Result<object>
                    .Fail(Error<Ingredient>.NotFound, HttpStatusCode.NotFound);
            }

            if (!ingredient.IsDeleted)
            {
                return Result<object>
                    .Fail(Error<Ingredient>.NotYetDeleted, HttpStatusCode.BadRequest);
            }

            ingredient.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<object>
                .Succeed(default, Success<Ingredient>.Restored);
        }
    }
}
