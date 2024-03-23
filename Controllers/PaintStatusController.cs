using Microsoft.AspNetCore.Mvc;
using PaintStockStatusAPI.Models;
using PaintStockStatusAPI.Services;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaintStatusController : ControllerBase
    {
        private readonly IPaintStatusService _paintService;

        public PaintStatusController(IPaintStatusService paintService)
        {
            _paintService = paintService;
        }


        private static PaintStatus ps = new PaintStatus();

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _paintService.GetAllPaints());
            //return Ok(ps);
        }
    }
}
