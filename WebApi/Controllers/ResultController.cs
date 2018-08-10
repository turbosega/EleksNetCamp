using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObjects.Creating;
using WebApi.Filters.Action;
using static WebApi.Helpers.ApiStringConstants;

namespace WebApi.Controllers
{
    [Route(StandartControllerRoute)]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService) => _resultService = resultService;

        [HttpGet("{id}")]
        [Authorize(Policy = AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetResultByIdAsync(int id) => Ok(await _resultService.GetByIdAsync(id));

        [HttpGet("all")]
        [Authorize(Policy = AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetAllResultsAsync() => Ok(await _resultService.GetAllAsync());

        [HttpPost("new")]
        [Authorize(Policy = AuthenticatedOnlyPolicy)]
        [UserSendsOnlyOwnScoreActionAsyncFilter]
        public async Task<IActionResult> CreateResultAsync([FromBody] ResultCreatingDto resultDto) => Ok(await _resultService.CreateAsync(resultDto));

        [HttpGet]
        [Authorize(Policy = AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetResultsByRelatedKeysAsync([FromQuery] int gameId, [FromQuery] int? nullableUserId = null) =>
            nullableUserId.HasValue && nullableUserId is int userId
                ? Ok(await _resultService.GetResultsByUserIdAndGameIdAsync(userId, gameId))
                : Ok(await _resultService.GetResultsByGameIdAsync(gameId));
    }
}