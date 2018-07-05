using Models.DataTransferObjects;
using Models.Entities;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService : IService<User, UserDto>
    {
    }
}