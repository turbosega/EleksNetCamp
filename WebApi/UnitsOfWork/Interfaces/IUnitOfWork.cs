using System.Threading.Tasks;
using WebApi.Repositories.Interfaces;

namespace WebApi.UnitsOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository  Users  { get; }
        IGameRepository  Games  { get; }
        IScoreRepository Scores { get; }

        Task SaveChangesAsync();
    }
}