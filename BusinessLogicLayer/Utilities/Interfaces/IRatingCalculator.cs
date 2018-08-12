using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferObjects;

namespace BusinessLogicLayer.Utilities.Interfaces
{
    public interface IRatingCalculator
    {
        Task<IEnumerable<(UserDto user, double ratingScore)>> GetUsersWithRatingsByGameIdAsync(int gameId, double minimumAmountOfResultsToTakePart);
    }
}