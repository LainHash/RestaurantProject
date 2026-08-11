using AutoMapper;
using Restaurant.Application.Features.Production.Recipes.Queries.GetAll;
using Restaurant.Application.Features.Production.Recipes.Queries.GetById;
using Restaurant.Application.Services.Production;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Production;
using System.Net;

namespace Restaurant.Persistence.Services.Production
{
    internal class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;

        public RecipeService(
            IRecipeRepository recipeRepository,
            IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
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
            if(recipe is null)
            {
                return Result<RecipeResponse>
                    .Fail(Error<Recipe>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<RecipeResponse>(recipe);
            return Result<RecipeResponse>
                .Succeed(response, Success<Recipe>.Retrieved);
        }
    }
}
