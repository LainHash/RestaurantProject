using AutoMapper;
using Restaurant.Application.Features.Production.Recipes.Queries.GetAll;
using Restaurant.Application.Services.Production;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Production;

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
    }
}
