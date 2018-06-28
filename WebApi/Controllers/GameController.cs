using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.DataTransferObjects;
using WebApi.Services.Interfaces;

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
        public async Task<IActionResult> GetGameByIdAsync(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

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
                return UnprocessableEntity("Something went wrong while creating new game");
            }

            return Ok(gameForSaving);
        }
    }
}