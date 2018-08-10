using System.Threading.Tasks;
using Models.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game, int>
    {
        Task<Game> GetByTitleAsync(string providedTitle);
    }
}