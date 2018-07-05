using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService) => _gameService = gameService;

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGameByIdAsync(int id) => Ok(await _gameService.GetByIdAsync(id));

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllGamesAsync() => Ok(await _gameService.GetAllAsync());

        [HttpPost("new")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateGameAsync([FromBody] GameDto gameDto)
        {
            var gameForSaving = await _gameService.CreateAsync(gameDto);
            if (gameForSaving == null)
            {
                return UnprocessableEntity();
            }

            return Ok(gameForSaving);
        }
    }
}