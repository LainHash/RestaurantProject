using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities.Personnel
{
    public class Employee : SoftDeletableEntity
    {
        public long EmployeeNumber { get; private set; }

        public string EmployeeCode =>
            $"EMP-{EmployeeNumber:D6}";

        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        public int PositionId { get; private set; }
        public Position Position { get; private set; } = null!;

        public DateTime HireDate { get; private set; }
        public DateTime? TerminationDate { get; private set; }

        public EmployeeStatus Status { get; private set; }
    }
}
