using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Exceptions;
using WebApi.Filters.Action;
using WebApi.Models.DataTransferObjects;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) => _accountService = accountService;

        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> LoginAsync([FromBody] UserDto userDto)
        {
            try
            {
                var token = await _accountService.AuthenticateAsync(userDto);
                return Ok(new
                {
                    userDto.Login,
                    token
                });
            }
            catch (BadCredentialsException e)
            {
                return Unauthorized();
            }
        }
    }
}