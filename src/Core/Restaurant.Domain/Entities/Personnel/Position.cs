using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Personnel
{
    public class Position : SoftDeletableEntity
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        public int DepartmentId { get; private set; }
        public Department Department { get; private set; } = null!;

        public ICollection<Employee> Employees { get; private set; } = [];
    }
}
