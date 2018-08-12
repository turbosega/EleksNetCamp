using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Utilities.Interfaces;

namespace BusinessLogicLayer.Utilities.Implementations
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

        public async Task CheckIfUserAndGameExist(int userId, int gameId)
        {
            var userCheckingTask = _userService.GetByIdAsync(userId);
            var gameCheckingTask = _gameService.GetByIdAsync(gameId);
            await Task.WhenAll(userCheckingTask, gameCheckingTask);
        }
    }
}