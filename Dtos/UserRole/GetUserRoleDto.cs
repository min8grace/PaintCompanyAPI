using PaintStockStatusAPI.Models;
using System.Collections.Generic;

namespace PaintStockStatusAPI.Dtos.UserRole
{
    public class GetUserRoleDto
    {
        public int RoleId { get; set; } // Primary key
        public string RoleName { get; set; }

        public List<GetUserPermissionDto> UserRolePermissions { get; set; }
    }
}
