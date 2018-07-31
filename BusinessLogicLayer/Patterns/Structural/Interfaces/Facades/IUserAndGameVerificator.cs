using System.Threading.Tasks;
using Models.DataTransferObjects;

namespace BusinessLogicLayer.Patterns.Structural.Interfaces.Facades
{
    public interface IUserAndGameVerificator
    {
        Task CheckIfUserAndGameExist(ResultDto resultDto);
    }
}