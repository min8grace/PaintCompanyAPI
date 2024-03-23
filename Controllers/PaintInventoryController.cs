using Microsoft.AspNetCore.Mvc;
using PaintStockStatusAPI.Dtos;
using PaintStockStatusAPI.Models;
using PaintStockStatusAPI.Services;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Controllers
{
    public class PaintInventoryController : ControllerBase
    {
        private readonly IPaintInventoryService _paintInventoryService;

        public PaintInventoryController(IPaintInventoryService paintInventoryService)
        {
            _paintInventoryService = paintInventoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _paintInventoryService.GetAllPaints());
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<IActionResult> GetPaintInventory(int ID)
        {
            return Ok(await _paintInventoryService.GetPaintInventoryById(ID));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaintInventoryByID(UpdatePaintInventoryDto updatePaintInventoryDto)
        {
            ServiceResponse<UpdatePaintInventoryDto> response = await _paintInventoryService.UpdatePaintInventory(updatePaintInventoryDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
