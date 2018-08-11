using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService : IService<UserDto, UserRegistrationDto, int>
    {
    }
}