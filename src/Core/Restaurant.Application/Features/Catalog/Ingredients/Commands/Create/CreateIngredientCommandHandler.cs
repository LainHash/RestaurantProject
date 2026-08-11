using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Create
{
    internal class CreateIngredientCommandHandler(IIngredientService ingredientService)
                : IRequestHandler<CreateIngredientCommand, Result<IngredientResponse>>
    {
        private readonly IIngredientService _ingredientService = ingredientService;

        public async Task<Result<IngredientResponse>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateIngredientSpecification(request);
            var response = await _ingredientService.CreateAsync(specification, request.Body, cancellationToken);
            return response;
        }
    }
}
