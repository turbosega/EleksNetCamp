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
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService) => _gameService = gameService;

        [HttpGet("{id}")]
        [Authorize(Policy = AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetGameByIdAsync(int id) => Ok(await _gameService.GetByIdAsync(id));

        [HttpGet("all")]
        [Authorize(Policy = AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetAllGamesAsync() => Ok(await _gameService.GetAllAsync());

        [HttpPost("new")]
        [Authorize(Policy = AdministratorsOnlyPolicy)]
        public async Task<IActionResult> CreateGameAsync([FromBody] GameCreatingDto gameDto) => Ok(await _gameService.CreateAsync(gameDto));

        [HttpGet("{id}/rating")]
        [Authorize(Policy = AdministratorsOnlyPolicy)]
        public async Task<IActionResult> GetUsersWithRatingsByGameIdAsync(int id) => Ok(await _gameService.GetUsersWithRatingsByGameIdAsync(id));
    }
}