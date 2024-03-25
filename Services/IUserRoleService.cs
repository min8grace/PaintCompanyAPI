using PaintStockStatusAPI.Dtos.User;
using PaintStockStatusAPI.Dtos.UserRole;
using PaintStockStatusAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Services
{
    public interface IUserRoleService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<List<GetUserRoleDto>>> GetAllUserRoles();
        Task<ServiceResponse<List<GetUserPermissionDto>>> GetAllUserPermissions();

        Task<ServiceResponse<GetUserDto>> GetUserById(int ID);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updateUserDto);
    }
}
