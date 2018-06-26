using System.Threading.Tasks;
using WebApi.Models.DataTransferObjects;

namespace WebApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(UserDto userDto);
    }
}