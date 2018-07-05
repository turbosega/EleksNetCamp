using System.Threading.Tasks;
using Models.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> GetByTitleAsync(string providedTitle);
    }
}