using MediatR;
using Restaurant.Application.Services.Production;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Queries.GetAll
{
    internal class GetAllRecipesQueryHandler(IRecipeService recipeService)
                : IRequestHandler<GetAllRecipesQuery, Result<IEnumerable<RecipeResponse>>>
    {
        private readonly IRecipeService _recipeService = recipeService;

        public async Task<Result<IEnumerable<RecipeResponse>>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllRecipesSpecification(request);
            var response = await _recipeService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
