using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Personnel
{
    public class Department : SoftDeletableEntity
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        public ICollection<Position> Positions { get; private set; } = [];
    }
}
