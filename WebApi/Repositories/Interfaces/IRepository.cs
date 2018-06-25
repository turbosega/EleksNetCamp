using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class // Represents base CRUD functionality
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task CreateAsync(TEntity entity);
    }
}