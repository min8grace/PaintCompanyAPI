using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaintStockStatusAPI.Models
{
    public enum UserRoleName
    {
        PaintStatusViewer,
        Manager,
        Painter,
        Admin
    }

    public enum UserPermissionType
    {
        ViewInventory,
        UpdateInventory,
        Order,
        ManageUsers,
    }

    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }
        public UserRoleName RoleName { get; set; }
        public UserPermissionType Permissions { get; set; }
    }
}
