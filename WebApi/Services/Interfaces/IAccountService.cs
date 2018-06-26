using System.Threading.Tasks;
using WebApi.Models.DataTransferObjects;

namespace WebApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> AuthenticateAsync(UserDto userDto);
    }
}