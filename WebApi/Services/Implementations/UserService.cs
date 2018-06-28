using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;
using WebApi.UnitsOfWork.Interfaces;

namespace WebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper     _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with id: {id} not found");
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _unitOfWork.Users.GetAllAsync();

        public async Task<User> CreateAsync(UserDto userDto)
        {
            if (await IsUserWithThisLoginExists(userDto.Login))
            {
                return null;
            }

            var userForSaving = MapFromDtoToUser(userDto);
            await _unitOfWork.Users.CreateAsync(userForSaving);
            await _unitOfWork.SaveChangesAsync();
            return userForSaving;
        }

        // private methods
        private async Task<bool> IsUserWithThisLoginExists(string providedLogin) => await _unitOfWork.Users.GetByLoginAsync(providedLogin) != null;

        private User MapFromDtoToUser(UserDto userDto) => _mapper.Map<User>(userDto);
    }
}