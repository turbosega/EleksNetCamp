using System.Threading.Tasks;

namespace WebApi.UnitsOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}