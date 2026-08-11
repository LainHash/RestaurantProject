using MediatR;
using Restaurant.Application.Services.Production;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Queries.GetById
{
    internal class GetRecipeByIdQueryHandler(IRecipeService recipeService)
                : IRequestHandler<GetRecipeByIdQuery, Result<RecipeResponse>>
    {
        private readonly IRecipeService _recipeService = recipeService;

        public async Task<Result<RecipeResponse>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetRecipeByIdSpecification(request);
            var response = await _recipeService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
