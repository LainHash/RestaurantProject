namespace Restaurant.Persistence.DataRecords.Territory
{
    internal class BranchRecord
    {
        public string City { get; set; } = null!;
        public string Code { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public string Status { get; set; } = null!;

        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }
}
