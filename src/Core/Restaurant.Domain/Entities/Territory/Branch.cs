using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities.Territory
{
    public class Branch : SoftDeletableEntity
    {
        public string City { get; private set; } = null!;
        public string Code { get; private set; } = null!;

        public string PhoneNumber { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        public string Address { get; private set; } = null!;

        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }

        public BranchStatus Status { get; private set; }

        public TimeOnly OpenTime { get; private set; }
        public TimeOnly CloseTime { get; private set; }

        public ICollection<ProductStock> ProductStocks { get; private set; } = [];
    }
}
