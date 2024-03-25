namespace PaintStockStatusAPI.Dtos.User
{
    public class GetUserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } 

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; }

    }
}
