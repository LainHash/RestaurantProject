using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Delete
{
    internal class DeleteIngredientCommandHandler(IIngredientService ingredientService)
                : IRequestHandler<DeleteIngredientCommand, Result<object>>
    {
        private readonly IIngredientService _ingredientService = ingredientService;

        public async Task<Result<object>> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteIngredientSpecification(request);
            var response = await _ingredientService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
