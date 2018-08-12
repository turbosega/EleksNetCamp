using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObjects.Creating;
using static WebApi.Helpers.ApiStringConstants;

namespace WebApi.Controllers
{
    [Route(StandartControllerRoute)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => _userService = userService;

        [HttpGet("{id}")]
        [Authorize(Policy = AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetUserByIdAsync(int id) => Ok(await _userService.GetByIdAsync(id));

        [HttpGet]
        [Authorize(Policy = AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetAllUsersAsync() => Ok(await _userService.GetAllAsync());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromForm] UserRegistrationDto userDto) => Ok(await _userService.CreateAsync(userDto));
    }
}