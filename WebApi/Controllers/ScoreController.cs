using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Action;
using WebApi.Models.DataTransferObjects;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService) => _scoreService = scoreService;

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetScoreByIdAsync(int id)
        {
            var score = await _scoreService.GetByIdAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            return Ok(score);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllScoresAsync() => Ok(await _scoreService.GetAllAsync());

        [HttpPost("new")]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IActionResult> CreateScoreAsync([FromBody] ScoreDto scoreDto)
        {
            var scoreForSaving = await _scoreService.CreateAsync(scoreDto);
            if (scoreForSaving == null)
            {
                return UnprocessableEntity();
            }

            return Ok(scoreForSaving);
        }
    }
}