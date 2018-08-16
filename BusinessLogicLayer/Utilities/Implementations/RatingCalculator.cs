using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Utilities.Interfaces;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Models.DataTransferObjects;
using Models.Entities;

namespace BusinessLogicLayer.Utilities.Implementations
{
    public class RatingCalculator : IRatingCalculator
    {
        private readonly IMapper     _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RatingCalculator(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper     = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserWithRatingDto>> GetUsersWithRatingsByGameIdAsync(
            int gameId, double minimumAmountOfResultsToTakePart)
        {
            var gameTask         = _unitOfWork.Games.GetByIdAsync(gameId);
            var participantsList = (await GetParticipants(gameId, minimumAmountOfResultsToTakePart)).ToList();
            return participantsList.Any()
                       ? CalculateRatingsForParticipants(participantsList,
                                                         gameId,
                                                         minimumAmountOfResultsToTakePart,
                                                         GetArithmeticMeanFromParticipants(participantsList),
                                                         GetCommonArithmeticMeanByGame(await gameTask),
                                                         GetAmountOfUserResultsPerGame)
                       : Enumerable.Empty<UserWithRatingDto>();
        }

        private async Task<IEnumerable<User>> GetParticipants(int gameId, double minimumAmountOfResultsToTakePart) =>
            await _unitOfWork.Users.GetByConditionAsync(user => user.Results.AsQueryable()
                                                                    .Count(result => result.GameId == gameId) >= minimumAmountOfResultsToTakePart);

        private double GetCommonArithmeticMeanByGame(Game game) => game.Results.AsQueryable().Average(result => result.Score);

        private double GetArithmeticMeanFromParticipants(IEnumerable<User> participants) => participants.AsQueryable()
                                                                                                        .SelectMany(user => user.Results)
                                                                                                        .Average(result => result.Score);

        private int GetAmountOfUserResultsPerGame(User user, int gameId) => user.Results.AsQueryable().Count(result => result.GameId == gameId);

        /* How it works:
         * rating score = (m / (v + m)) * c + (v / (v + m)) * r 
         * where:
         * m = minimum amount of results to take part 
         * v = amount of user results per game (it's function in code)
         * c = common arithmetic mean of results per game(calculated by getting average number of all results)
         * r = arithmetic mean from users who take part in rating(who have M and more results per game)
         */
        private IEnumerable<UserWithRatingDto> CalculateRatingsForParticipants(IEnumerable<User> participantsList,
                                                                               int gameId,
                                                                               double minAmountOfScores,
                                                                               double participantsArithmMean,
                                                                               double commonArithmMean,
                                                                               Func<User, int, int> amountOfScores)
        {
            var tuple = participantsList.Select(
                                                (user, ratingScore) =>
                                                    (
                                                        user,
                                                        minAmountOfScores /
                                                        (amountOfScores(user, gameId) + minAmountOfScores) *
                                                        commonArithmMean +
                                                        amountOfScores(user, gameId) /
                                                        (amountOfScores(user, gameId) + minAmountOfScores) *
                                                        participantsArithmMean
                                                    )
                                               );

            return _mapper.Map<IEnumerable<UserWithRatingDto>>(tuple);
        }
    }
}