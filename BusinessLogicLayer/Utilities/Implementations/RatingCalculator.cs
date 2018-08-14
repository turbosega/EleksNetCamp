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

            var game = await gameTask;
            return CalculateRatingsForParticipants(participantsList,
                                                   gameId,
                                                   minimumAmountOfResultsToTakePart,
                                                   GetArithmeticMeanFromParticipants(participantsList),
                                                   GetCommonArithmeticMeanByGame(game),
                                                   GetAmountOfUserResultsPerGame);
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
         * rating = (v / (v + m)) * r + (m / (v + m)) * c
         * where:
         * v = function which calculates amount of user results per game 
         * m = minimum amount of results to take part
         * r = arithmetic mean from users who take part in rating(who have M and more results per game)
         * c = common arithmetic mean of results per game(calculated by getting average number of all results)
         * TODO: look for better ways of calculating
         */
        private IEnumerable<(UserDto user, double rating)> CalculateRatingsForParticipants(IEnumerable<User> participantsList,
                                                                                           int gameId,
                                                                                           double m,
                                                                                           double r,
                                                                                           double c,
                                                                                           Func<User, int, int> v)
        {
            v = GetAmountOfUserResultsPerGame;
            return participantsList.Select((user, rating) =>
                                               (_mapper.Map<UserDto>(user),
                                                v(user, gameId) / (v(user, gameId) + m) * r +
                                                m               / (v(user, gameId) + m) * c));
        }
    }
}