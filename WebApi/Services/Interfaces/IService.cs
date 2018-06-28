using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Services.Interfaces
{
    public interface IService<TEntity, in TEntityDto> where TEntity : class
                                                      where TEntityDto : class
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> CreateAsync(TEntityDto entityDto);
    }
}