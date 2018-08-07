using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IResultRepository : IRepository<Result>
    {
        Task<IEnumerable<Result>> GetResultsByUserIdAsync(int userId);

        Task<IEnumerable<Result>> GetResultsByGameIdAsync(int gameId);

        Task<IEnumerable<Result>> GetResultsByUserIdAndGameIdAsync(int userId, int gameId);
    }
}