using PaintStockStatusAPI.Dtos;
using PaintStockStatusAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Services
{
    public interface IPaintStatusService
    {        
        Task<ServiceResponse<List<GetPaintStatusDto>>> GetAllPaints();
    }
}
