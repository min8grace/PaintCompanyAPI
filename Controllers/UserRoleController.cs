using Microsoft.AspNetCore.Mvc;
using PaintStockStatusAPI.Dtos;
using PaintStockStatusAPI.Models;
using PaintStockStatusAPI.Services;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleSerivce;

        public UserRoleController(IUserRoleService userRoleSerivce)
        {
            _userRoleSerivce = userRoleSerivce;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userRoleSerivce.GetAllUsers());
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _userRoleSerivce.GetAllUserRoles());
        }

        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            return Ok(await _userRoleSerivce.GetAllUserPermissions());
        }
    }
}
