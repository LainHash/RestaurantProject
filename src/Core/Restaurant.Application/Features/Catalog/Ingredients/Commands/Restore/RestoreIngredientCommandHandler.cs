using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Restore
{
    internal class RestoreIngredientCommandHandler(IIngredientService ingredientService)
                : IRequestHandler<RestoreIngredientCommand, Result<object>>
    {
        private readonly IIngredientService _ingredientService = ingredientService;

        public async Task<Result<object>> Handle(RestoreIngredientCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreIngredientSpecification(request);
            var response = await _ingredientService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
