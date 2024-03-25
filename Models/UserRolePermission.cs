namespace PaintStockStatusAPI.Models
{
    // Model to represent the many-to-many relationship between user roles and permissions

    public class UserRolePermission
    {
        public int UserRolePermissionId { get; set; } // Primary key
        public int UserRoleId { get; set; } // Foreign key to UserRole
        public UserRole UserRole { get; set; } // Navigation property
        public int PermissionId { get; set; } // Foreign key to Permission
        public UserPermission Permission { get; set; } // Navigation property
    }
}
