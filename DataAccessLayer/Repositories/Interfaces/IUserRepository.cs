using System.Threading.Tasks;
using Models.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByLoginAsync(string providedLogin);
    }
}