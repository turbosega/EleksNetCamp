using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IGameService : IService<GameDto, GameCreatingDto>
    {
        Task<IEnumerable<(UserDto user, double rating)>> GetRatingsByGameIdAsync(int gameId);
    }
}