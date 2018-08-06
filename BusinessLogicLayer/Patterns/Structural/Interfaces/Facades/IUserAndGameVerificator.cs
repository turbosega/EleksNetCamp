using System.Threading.Tasks;

namespace BusinessLogicLayer.Patterns.Structural.Interfaces.Facades
{
    public interface IUserAndGameVerificator
    {
        Task CheckIfUserAndGameExist(int userId, int gameId);
    }
}