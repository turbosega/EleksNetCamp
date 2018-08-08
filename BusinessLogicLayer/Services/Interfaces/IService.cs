using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IService<TEntityDto, TCreatingDto> where TEntityDto : class
                                                        where TCreatingDto : class

    {
        Task<TEntityDto> GetByIdAsync(int id);

        Task<IEnumerable<TEntityDto>> GetAllAsync();

        Task<TEntityDto> CreateAsync(TCreatingDto entityDto);
    }
}