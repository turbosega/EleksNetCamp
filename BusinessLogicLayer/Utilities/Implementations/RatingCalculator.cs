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

        /*
         * How it works:
         * rating = (V / (V + M)) * R + (M / (V + M)) * C
         * where:
         * V = amount of user results per game
         * M = minimum amount of results to take part
         * R = arithmetic mean from users who take part in rating (who have M and more results per game)
         * C = common arithmetic mean of results per game (calculated by getting average number of all results)
         *
         * calculating is in CalculateRatingsForParticipants method
         * enjoy ^^
         */
        public async Task<IEnumerable<(UserDto user, double ratingScore)>> GetUsersWithRatingsByGameIdAsync(
            int gameId, double minimumAmountOfResultsToTakePart)
        {
            var gameTask                    = _unitOfWork.Games.GetByIdAsync(gameId);
            var usersWhoParticipateInRating = await GetParticipants(gameId, minimumAmountOfResultsToTakePart);
            var participantsList            = usersWhoParticipateInRating.ToList();
            if (!participantsList.Any())
            {
                return Enumerable.Empty<(UserDto, double)>();
            }

            var game                 = await gameTask;
            var commonArithmeticMean = GetCommonArithmeticMeanByGame(game);

            var arithmeticMeanFromParticipants = GetArithmeticMeanFromParticipants(participantsList);

            return CalculateRatingsForParticipants(participantsList,
                                                   gameId,
                                                   minimumAmountOfResultsToTakePart,
                                                   arithmeticMeanFromParticipants,
                                                   commonArithmeticMean);
        }

        private async Task<IEnumerable<User>> GetParticipants(int gameId, double minimumAmountOfResultsToTakePart) =>
            await _unitOfWork.Users.GetByConditionAsync(user => user.Results.AsQueryable()
                                                                    .Count(result => result.GameId == gameId) >= minimumAmountOfResultsToTakePart);

        private double GetCommonArithmeticMeanByGame(Game game) => game.Results.AsQueryable().Average(result => result.Score);

        private double GetArithmeticMeanFromParticipants(IEnumerable<User> participants) => participants.AsQueryable()
                                                                                                        .SelectMany(user => user.Results)
                                                                                                        .Average(result => result.Score);

        // it looks beautiful, i know :D
        // TODO: refactor someday in future
        private IEnumerable<(UserDto user, double rating)> CalculateRatingsForParticipants(IEnumerable<User> participantsList,
                                                                                           int gameId,
                                                                                           double minimumAmountOfResultsToTakePart,
                                                                                           double arithmeticMeanFromUsersWhoParticipateInRating,
                                                                                           double commonArithmeticMean) =>
            participantsList.Select((user, rating) =>
                                        (_mapper.Map<UserDto>(user),
                                         user.Results.AsQueryable().Count(result => result.GameId == gameId) /
                                         (user.Results.AsQueryable().Count(result => result.GameId == gameId) +
                                          minimumAmountOfResultsToTakePart) *
                                         arithmeticMeanFromUsersWhoParticipateInRating +
                                         minimumAmountOfResultsToTakePart /
                                         (minimumAmountOfResultsToTakePart +
                                          user.Results.AsQueryable().Count(result => result.GameId == gameId)) *
                                         commonArithmeticMean));
    }
}