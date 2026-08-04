using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Persistence.Services.Catalog
{
    internal class CategoryService : ICategoryService
    {
        public Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
