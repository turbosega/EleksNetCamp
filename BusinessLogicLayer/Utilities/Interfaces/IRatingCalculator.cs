using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferObjects;

namespace BusinessLogicLayer.Utilities.Interfaces
{
    public interface IRatingCalculator
    {
        Task<IEnumerable<UserWithRatingDto>> GetUsersWithRatingsByGameIdAsync(int gameId, double minimumAmountOfResultsToTakePart);
    }
}