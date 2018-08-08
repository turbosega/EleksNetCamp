using System.Threading.Tasks;
using Models.DataTransferObjects.Creating;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(UserAuthDto authDto);
    }
}