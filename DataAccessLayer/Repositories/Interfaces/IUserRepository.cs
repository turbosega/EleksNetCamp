using System.Threading.Tasks;
using Models.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetByLoginAsync(string providedLogin);
    }
}