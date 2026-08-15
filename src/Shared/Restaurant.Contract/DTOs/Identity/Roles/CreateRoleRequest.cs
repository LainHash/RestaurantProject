namespace Restaurant.Contract.DTOs.Identity.Roles
{
    public class CreateRoleRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
