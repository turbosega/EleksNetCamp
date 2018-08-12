using System.Threading.Tasks;

namespace BusinessLogicLayer.Utilities.Interfaces
{
    public interface IUserAndGameVerificator
    {
        Task CheckIfUserAndGameExist(int userId, int gameId);
    }
}