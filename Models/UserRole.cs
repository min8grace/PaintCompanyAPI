using System.Collections.Generic;

namespace PaintStockStatusAPI.Models
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public UserRoleType RoleName { get; set; }
        public List<UserPermissionType> Permissions { get; set; }
    }
}
