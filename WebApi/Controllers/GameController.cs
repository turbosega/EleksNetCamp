using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObjects;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route(ApiStringConstants.StandartControllerRoute)]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService) => _gameService = gameService;

        [HttpGet("{id}")]
        [Authorize(Policy = ApiStringConstants.AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetGameByIdAsync(int id) => Ok(await _gameService.GetByIdAsync(id));

        [HttpGet("all")]
        [Authorize(Policy = ApiStringConstants.AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetAllGamesAsync() => Ok(await _gameService.GetAllAsync());

        [HttpPost("new")]
        //[Authorize(Policy = ApiStringConstants.AdministratorsOnlyPolicy)]
        public async Task<IActionResult> CreateGameAsync([FromBody] GameDto gameDto) => Ok(await _gameService.CreateAsync(gameDto));
        
    }
}