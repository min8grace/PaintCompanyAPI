namespace PaintStockStatusAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }= string.Empty;
        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        // Navigation property for relationship with UserRole
        public UserRole? UserRole { get; set; }

    }
}
