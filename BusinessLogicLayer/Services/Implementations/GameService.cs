using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;
using Models.Entities;

namespace BusinessLogicLayer.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IMapper     _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper     = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GameDto> GetByIdAsync(int id) => _mapper.Map<GameDto>(await _unitOfWork.Games.GetByIdAsync(id)) ??
                                                           throw new ResourceNotFoundException($"Game with {nameof(id)}: {id} not found");


        public async Task<IEnumerable<GameDto>> GetAllAsync() => _mapper.Map<IEnumerable<GameDto>>(await _unitOfWork.Games.GetAllAsync());

        public async Task<GameDto> CreateAsync(GameCreatingDto gameDto)
        {
            if (await DoesGameWithThisTitleExist(gameDto.Title))
            {
                throw new NameOfResourceIsTakenException($"Game with title: {gameDto.Title} already exists");
            }

            var gameForSaving = _mapper.Map<Game>(gameDto);
            await _unitOfWork.Games.CreateAsync(gameForSaving);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GameDto>(gameForSaving);
        }

        /*
         * not sure if it is the best way to solve this business task
         * but want to try to use tuples
         */
        public async Task<IEnumerable<(UserDto user, double rating)>> GetRatingsByGameIdAsync(int gameId)
        {
            const double minimumResultsToTakePart = 5;
            var          game                     = await _unitOfWork.Games.GetByIdAsync(gameId);
            var          allGameResultsLength     = game.Results.Count();
            var          commonArithmeticMean     = game.Results.Average(r => r.Score);
            var usersWhichTakePartInRating = await _unitOfWork.Users.GetByConditionAsync(user => user.Results
                                                                                                     .AsQueryable()
                                                                                                     .Count(result => result.GameId == gameId) >=
                                                                                                 minimumResultsToTakePart);
            var usersWhichTakePartInRatingArithmeticMean =
                usersWhichTakePartInRating.ToImmutableList().AsQueryable().SelectMany(user => user.Results.Where(r => r.GameId == gameId))
                                          .Average(r => r.Score);

            var finalResult = usersWhichTakePartInRating.ToImmutableList().Select((user, rating) =>
                                                                                      (_mapper.Map<UserDto>(user),
                                                                                       user.Results.Count(r => r.GameId == gameId) /
                                                                                       (user.Results.Count(r => r.GameId == gameId) +
                                                                                        minimumResultsToTakePart) *
                                                                                       usersWhichTakePartInRatingArithmeticMean +
                                                                                       minimumResultsToTakePart / (allGameResultsLength +
                                                                                                                   allGameResultsLength) *
                                                                                       commonArithmeticMean));
            return finalResult;
        }

        private async Task<bool> DoesGameWithThisTitleExist(string providedTitle) => await _unitOfWork.Games.GetByTitleAsync(providedTitle) != null;
    }
}