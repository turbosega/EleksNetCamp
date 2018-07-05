﻿using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserDto userDto)
        {
            var token = await _authService.AuthenticateAsync(userDto);
            return Ok(new
            {
                userDto.Login,
                token
            });
        }
    }
}