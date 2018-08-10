using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IService<TEntityDto, in TCreatingDto, in TKey> where TEntityDto : class
                                                                    where TCreatingDto : class

    {
        Task<TEntityDto> GetByIdAsync(TKey id);

        Task<IEnumerable<TEntityDto>> GetAllAsync();

        Task<TEntityDto> CreateAsync(TCreatingDto entityDto);
    }
}