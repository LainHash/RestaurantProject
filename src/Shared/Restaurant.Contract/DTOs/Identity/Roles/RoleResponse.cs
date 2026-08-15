namespace Restaurant.Contract.DTOs.Identity.Roles
{
    public class RoleResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
