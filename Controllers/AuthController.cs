using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaintStockStatusAPI.Data;
using PaintStockStatusAPI.Dtos.User;
using PaintStockStatusAPI.Dtos.UserRole;
using PaintStockStatusAPI.Models;
using PaintStockStatusAPI.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IUserRoleService _userRoleService;
        public AuthController(IAuthRepository authRepository, IUserRoleService userRoleService)
        {
            _authRepo = authRepository;
            _userRoleService = userRoleService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            ServiceResponse<string> response = await _authRepo.Register(
                new User { Username = request.Username }, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            ServiceResponse<string> response = await _authRepo.Login(
                request.Username, request.Password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetMe")]
        public async Task<IActionResult> GetMe()
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();

            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            response = await _userRoleService.GetUserById(userId);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
