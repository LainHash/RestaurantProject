using MediatR;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Queries.GetById
{
    internal class GetIngredientByIdQueryHandler(IIngredientService ingredientService)
        : IRequestHandler<GetIngredientByIdQuery, Result<IngredientResponse>>
    {
        private readonly IIngredientService _ingredientService = ingredientService;

        public async Task<Result<IngredientResponse>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetIngredientByIdSpecification(request);
            var response = await _ingredientService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
