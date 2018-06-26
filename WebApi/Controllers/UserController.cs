using System.Collections.Generic;
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<User> GetUserByIdAsync(int id) => await _userService.GetUserByIdAsync(id);

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userService.GetAllUsersAsync();

        [HttpPost("rgstr")]
        [AllowAnonymous]
        public async Task<User> CreateUserAsync(UserDto userDto) => await _userService.CreateUserAsync(userDto);
    }
}