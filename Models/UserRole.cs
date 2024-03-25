using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaintStockStatusAPI.Models
{
    public enum RoleName
    {
        Viewer =1,
        Manager =2,
        Painter =3,
        Admin =4,
    }

 
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; } // Primary key
        public string RoleName { get; set; }
        public List<UserRolePermission> UserRolePermissions { get; set; } // Navigation property for many-to-many relationship

    }
}
