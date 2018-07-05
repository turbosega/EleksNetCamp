using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Models.DataTransferObjects;
using Models.Entities;

namespace BusinessLogicLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper     _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper     = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with {nameof(id)}: {id} not found");
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _unitOfWork.Users.GetAllAsync();

        public async Task<User> CreateAsync(UserDto userDto)
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

        private async Task<bool> IsUserWithThisLoginExists(string providedLogin) => await _unitOfWork.Users.GetByLoginAsync(providedLogin) != null;

        private User MapFromDtoToUser(UserDto userDto) => _mapper.Map<User>(userDto);
    }
}