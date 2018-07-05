using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.UnitsOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository   Users   { get; }
        IGameRepository   Games   { get; }
        IResultRepository Results { get; }

        Task SaveChangesAsync();
    }
}