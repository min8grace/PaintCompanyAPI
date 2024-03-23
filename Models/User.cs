namespace PaintStockStatusAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UserRoleType Role { get; set; }

        public bool IsActive { get; set; }

    }
}
