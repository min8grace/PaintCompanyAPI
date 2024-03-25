using PaintStockStatusAPI.Dtos;
using PaintStockStatusAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Services
{
    public interface IPaintInventoryService
    {        
        Task<ServiceResponse<List<GetPaintInventoryDto>>> GetAllPaints();
        Task<ServiceResponse<GetPaintInventoryDto>> GetPaintInventoryById(int ID);
        Task<ServiceResponse<GetPaintInventoryDto>> UpdatePaintInventory(UpdatePaintInventoryDto updatePaintInventoryDto);
    }
}
