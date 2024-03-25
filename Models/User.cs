namespace PaintStockStatusAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        // Navigation property for relationship with UserRole
        public UserRole? UserRole { get; set; }

    }
}
