namespace PaintStockStatusAPI.Dtos.User
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; }
    }
}
