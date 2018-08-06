using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferObjects;
using Models.Entities;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IResultService : IService<Result, ResultDto>
    {
        Task<IEnumerable<Result>> GetResultsByUserIdAndGameIdAsync(int userId, int gameId);
    }
}