namespace Restaurant.Persistence.DataRecords.Personnel
{
    internal class PositionRecord
    {
        public string DepartmentName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
