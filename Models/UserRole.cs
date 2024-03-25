using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaintStockStatusAPI.Models
{
    public enum RoleName
    {
        Viewer,
        Manager,
        Painter,
        Admin
    }

 
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; } // Primary key
        public string RoleName { get; set; }
        public List<UserRolePermission> UserRolePermissions { get; set; } // Navigation property for many-to-many relationship

    }
}
