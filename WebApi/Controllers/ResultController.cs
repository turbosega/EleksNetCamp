using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObjects.Creating;
using WebApi.Filters.Action;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route(ApiStringConstants.StandartControllerRoute)]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService) => _resultService = resultService;

        [HttpGet("{id}")]
        [Authorize(Policy = ApiStringConstants.AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetResultByIdAsync(int id) => Ok(await _resultService.GetByIdAsync(id));

        [HttpGet("all")]
        [Authorize(Policy = ApiStringConstants.AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetAllResultsAsync() => Ok(await _resultService.GetAllAsync());

        [HttpPost("new")]
        [Authorize(Policy = ApiStringConstants.AuthenticatedOnlyPolicy)]
        [UserSendsOnlyOwnScoreActionAsyncFilter]
        public async Task<IActionResult> CreateResultAsync([FromBody] ResultCreatingDto resultDto) => Ok(await _resultService.CreateAsync(resultDto));

        [HttpGet]
        [Authorize(Policy = ApiStringConstants.AuthenticatedOnlyPolicy)]
        public async Task<IActionResult> GetResultsByUserIdAndGameIdAsync(int userId, int gameId) =>
            Ok(await _resultService.GetResultsByUserIdAndGameIdAsync(userId, gameId));
    }
}