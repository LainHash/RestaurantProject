using MediatR;
using Restaurant.Application.Services.Production;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Queries.GetAllByProductId
{
    internal class GetAllRecipesByProductIdQueryHandler(IRecipeService recipeService)
                : IRequestHandler<GetAllRecipesByProductIdQuery, Result<IEnumerable<RecipeResponse>>>
    {
        private readonly IRecipeService _recipeService = recipeService;

        public async Task<Result<IEnumerable<RecipeResponse>>> Handle(GetAllRecipesByProductIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllRecipesByProductIdSpecification(request);
            var response = await _recipeService.GetAllByProductIdAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
