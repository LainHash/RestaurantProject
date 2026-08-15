namespace Restaurant.Contract.DTOs.Identity.Roles
{
    public class UpdateRoleRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
