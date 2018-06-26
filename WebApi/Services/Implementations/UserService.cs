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

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException($"User with id: {id} not found");
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _unitOfWork.Users.GetAllAsync();

        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            if (await IsUserWithThisLoginExists(userDto.Login))
            {
                throw new LoginIsTakenException($"User with login: {userDto.Login} already exists");
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