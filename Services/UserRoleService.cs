using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaintStockStatusAPI.Data;
using PaintStockStatusAPI.Dtos.User;
using PaintStockStatusAPI.Dtos.UserRole;
using PaintStockStatusAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRoleService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new ServiceResponse<List<GetUserDto>>();

            List<User> dbUsers = await _context.User.ToListAsync();
            serviceResponse.Data = dbUsers.Select(o => _mapper.Map<GetUserDto>(o)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserRoleDto>>> GetAllUserRoles()
        {
            ServiceResponse<List<GetUserRoleDto>> serviceResponse = new ServiceResponse<List<GetUserRoleDto>>();

            List<UserRole> dbUserRoles = await _context.UserRole.ToListAsync();
            List<UserRolePermission> dbUserRolePermission = await _context.UserRolePermission.ToListAsync();

            serviceResponse.Data = new List<GetUserRoleDto>();

            foreach (var dbUserRole in dbUserRoles)
            {
                var userRolePermissions = dbUserRolePermission.FindAll(x => x.UserRoleId == dbUserRole.RoleId);

                GetUserRoleDto getUserRoleDto = new GetUserRoleDto();
                getUserRoleDto.RoleId = dbUserRole.RoleId;  
                getUserRoleDto.RoleName = dbUserRole.RoleName;
                getUserRoleDto.UserRolePermissions = new List<GetUserPermissionDto>();

                foreach (var userRolePermission in userRolePermissions)
                {
                    GetUserPermissionDto getUserPermissionDto = new GetUserPermissionDto();
                    getUserPermissionDto.PermissionId = userRolePermission.PermissionId;

                    // Add PermissionType enum value as stirng
                    PermissionType val = (PermissionType)userRolePermission.PermissionId;
                    getUserPermissionDto.Name = val.ToString(); 
                    getUserRoleDto.UserRolePermissions.Add(getUserPermissionDto);
                }
                serviceResponse.Data.Add(getUserRoleDto);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserPermissionDto>>> GetAllUserPermissions()
        {
            ServiceResponse<List<GetUserPermissionDto>> serviceResponse = new ServiceResponse<List<GetUserPermissionDto>>();

            List<UserPermission> dbUserPermissions = await _context.UserPermission.ToListAsync();
            serviceResponse.Data = dbUserPermissions.Select(o => _mapper.Map<GetUserPermissionDto>(o)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int ID)
        {
            ServiceResponse<GetUserDto> serviceResponse = new ServiceResponse<GetUserDto>();
            User dbUser = await _context.User.FirstOrDefaultAsync(o => (int)o.UserId == ID);
            serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            return serviceResponse;

        }
        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updateUserDto)
        {
            ServiceResponse<GetUserDto> serviceResponse = new ServiceResponse<GetUserDto>();
            try
            {
                User dbUser = await _context.User.FirstOrDefaultAsync(o => o.UserId == updateUserDto.UserId);
                dbUser.Username = updateUserDto.Username;
                dbUser.RoleId = updateUserDto.RoleId;
                dbUser.IsActive = updateUserDto.IsActive;

                _context.User.Update(dbUser);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
