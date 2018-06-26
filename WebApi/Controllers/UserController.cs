using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Action;
using WebApi.Models.DataTransferObjects;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUsersAsync() => Ok(await _userService.GetAllUsersAsync());

        [HttpPost("rgstr")]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IActionResult> CreateUserAsync(UserDto userDto)
        {
            var savedUser = await _userService.CreateUserAsync(userDto);
            if (savedUser == null)
            {
                return UnprocessableEntity();
            }

            return Ok(savedUser);
        }
    }
}