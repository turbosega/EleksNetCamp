using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IGameService : IService<GameDto, GameCreatingDto, int>
    {
        // using tuples just because of curiosity, it's not the best way
        Task<IEnumerable<UserWithRatingDto>> GetUsersWithRatingsByGameIdAsync(int gameId);
    }
}