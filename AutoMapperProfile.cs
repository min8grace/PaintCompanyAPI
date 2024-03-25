using AutoMapper;
using PaintStockStatusAPI.Dtos;
using PaintStockStatusAPI.Dtos.User;
using PaintStockStatusAPI.Dtos.UserRole;

namespace PaintStockStatusAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.PaintInventory, GetPaintInventoryDto>();
            CreateMap<UpdatePaintInventoryDto, Models.PaintInventory>();
            CreateMap<Models.User, GetUserDto>();
            CreateMap<Models.UserRole, GetUserRoleDto>();
            CreateMap<Models.UserPermission, GetUserPermissionDto>();
            CreateMap<GetUserPermissionDto, Models.UserPermission>();
        }
    }
}
