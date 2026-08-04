using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Services.Catalog
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync();
    }
}
