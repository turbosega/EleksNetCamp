﻿using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpGet("{id}")]
        [Authorize("AuthenticatedOnly")]
        public async Task<IActionResult> GetUserByIdAsync(int id) => Ok(await _userService.GetByIdAsync(id));

        [HttpGet("all")]
        [Authorize("AuthenticatedOnly")]
        public async Task<IActionResult> GetAllUsersAsync() => Ok(await _userService.GetAllAsync());

        [HttpPost("rgstr")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userDto) => Ok(await _userService.CreateAsync(userDto));
    }
}