using AutoMapper;
using PaintStockStatusAPI.Dtos;

namespace PaintStockStatusAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.PaintInventory, GetPaintInventoryDto>();
            CreateMap<UpdatePaintInventoryDto, Models.PaintInventory>();
        }
    }
}
