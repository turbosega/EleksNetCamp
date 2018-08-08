using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IResultService : IService<ResultDto, ResultCreatingDto>
    {
        Task<IEnumerable<ResultDto>> GetResultsByUserIdAndGameIdAsync(int userId, int gameId);
    }
}