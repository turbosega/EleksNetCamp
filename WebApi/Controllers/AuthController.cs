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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserAuthDto authDto)
        {
            var token = await _authService.AuthenticateAsync(authDto);
            return Ok(new
            {
                authDto.Login,
                token
            });
        }
    }
}