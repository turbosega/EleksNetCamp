using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> CreateUserAsync(UserDto userDto);
    }
}