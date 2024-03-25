using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaintStockStatusAPI.Dtos;
using PaintStockStatusAPI.Dtos.User;
using PaintStockStatusAPI.Models;
using PaintStockStatusAPI.Services;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Controllers
{
    [Authorize]
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

        [HttpGet]
        [Route("GetUserById/{ID}")]
        public async Task<IActionResult> GetUser(int ID)
        {
            return Ok(await _userRoleSerivce.GetUserById(ID));
        }


        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _userRoleSerivce.GetAllUserRoles());
        }

        //[HttpGet]
        //[Route("GetRoleById/{ID}")]
        //public async Task<IActionResult> GetRole(int ID)
        //{
        //    return Ok(await _userRoleSerivce.GetRoleId(ID));
        //}

        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            return Ok(await _userRoleSerivce.GetAllUserPermissions());
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            ServiceResponse<GetUserDto> response = await _userRoleSerivce.UpdateUser(updateUserDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
