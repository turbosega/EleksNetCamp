using System.Threading.Tasks;
using BusinessLogicLayer.Patterns.Structural.Interfaces.Facades;
using BusinessLogicLayer.Services.Interfaces;
using Models.DataTransferObjects;

namespace BusinessLogicLayer.Patterns.Structural.Implementations.Facades
{
    public class UserAndGameVerificator : IUserAndGameVerificator
    {
        private readonly IUserService _userService;
        private readonly IGameService _gameService;

        public UserAndGameVerificator(IUserService userService, IGameService gameService)
        {
            _userService = userService;
            _gameService = gameService;
        }

        public async Task<bool> CheckIfUserAndGameExist(ResultDto resultDto) =>
            await _userService.GetByIdAsync(resultDto.UserId) != null && await _gameService.GetByIdAsync(resultDto.GameId) != null;
    }
}