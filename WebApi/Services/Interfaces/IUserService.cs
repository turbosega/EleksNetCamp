using System.Threading.Tasks;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserDto userDto);
    }
}