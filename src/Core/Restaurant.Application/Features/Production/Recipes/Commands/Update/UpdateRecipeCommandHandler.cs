using MediatR;
using Restaurant.Application.Services.Production;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Commands.Update
{
    internal class UpdateRecipeCommandHandler(IRecipeService recipeService)
                : IRequestHandler<UpdateRecipeCommand, Result<RecipeResponse>>
    {
        private readonly IRecipeService _recipeService = recipeService;

        public async Task<Result<RecipeResponse>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateRecipeSpecification(request);
            var response = await _recipeService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
