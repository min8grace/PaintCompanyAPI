using System;
using System.Collections.Generic;

namespace PaintStockStatusAPI.Models
{
    public enum PermissionType
    {
        ViewInventory = 1,
        UpdateInventory = 2,
        ManageUsers =3 ,
    }


    public class UserPermission
    {
        public int PermissionId { get; set; } // Primary key
        public string Name { get; set; }
        public List<UserRolePermission> UserRolePermissions { get; set; } // Navigation property for many-to-many relationship

        public string GetUserPermissionString
        {
            get
            {
                if (Enum.IsDefined(typeof(PermissionType), PermissionId))
                {
                    // Convert the enum value to its string representation
                    return Enum.GetName(typeof(PermissionType), PermissionId);
                }
                else
                {
                    // If the provided type is not valid, return an empty string or handle the error accordingly
                    return string.Empty;
                }
            }
        }
    }
}
