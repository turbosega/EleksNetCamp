using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService) => _resultService = resultService;

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResultByIdAsync(int id) => Ok(await _resultService.GetByIdAsync(id));

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllResultsAsync() => Ok(await _resultService.GetAllAsync());

        [HttpPost("new")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateResultAsync([FromBody] ResultDto resultDto)
        {
            var resultForSaving = await _resultService.CreateAsync(resultDto);
            if (resultForSaving == null)
            {
                return UnprocessableEntity();
            }

            return Ok(resultForSaving);
        }
    }
}