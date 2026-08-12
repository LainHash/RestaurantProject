using MediatR;
using Restaurant.Application.Services.Production;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Commands.Create
{
    internal class CreateRecipeCommandHandler(IRecipeService recipeService)
                : IRequestHandler<CreateRecipeCommand, Result<RecipeResponse>>
    {
        private readonly IRecipeService _recipeService = recipeService;

        public async Task<Result<RecipeResponse>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateRecipeSpecification(request);
            var response = await _recipeService.CreateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
