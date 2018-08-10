using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferObjects;

namespace BusinessLogicLayer.Utilities
{
    public interface IRatingCalculator
    {
        Task<IEnumerable<(UserDto user, double ratingScore)>> GetUsersWithRatingsByGameIdAsync(int gameId, double minimumAmountOfResultsToTakePart);
    }
}