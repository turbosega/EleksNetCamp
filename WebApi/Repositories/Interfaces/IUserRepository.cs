using System.Threading.Tasks;
using WebApi.Models.Entities;

namespace WebApi.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByLoginAsync(string login);
    }
}