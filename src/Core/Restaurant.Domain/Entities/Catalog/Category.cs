using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Category : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
    }
}
