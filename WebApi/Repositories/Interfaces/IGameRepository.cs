using System.Threading.Tasks;
using WebApi.Models.Entities;

namespace WebApi.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> GetByTitleAsync(string title);
    }
}