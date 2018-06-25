using System.Threading.Tasks;
using WebApi.Repositories.Interfaces;

namespace WebApi.UnitsOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        Task SaveChangesAsync();
    }
}