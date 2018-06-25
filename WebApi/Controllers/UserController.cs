using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpPost("rgstr")]
        [AllowAnonymous]
        public async Task<User> CreateUserAsync(UserDto userDto) => await _userService.CreateUserAsync(userDto);
    }
}