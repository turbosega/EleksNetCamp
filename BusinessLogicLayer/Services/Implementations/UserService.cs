using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Utilities;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;
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

        public async Task<UserDto> GetByIdAsync(int id) => _mapper.Map<UserDto>(await _unitOfWork.Users.GetByIdAsync(id)) ??
                                                           throw new ResourceNotFoundException($"User with {nameof(id)}: {id} not found");

        public async Task<IEnumerable<UserDto>> GetAllAsync() => _mapper.Map<IEnumerable<UserDto>>(await _unitOfWork.Users.GetAllAsync());

        public async Task<UserDto> CreateAsync(UserRegistrationDto userDto)
        {
            if (await DoesUserWithThisLoginExist(userDto.Login))
            {
                throw new LoginIsTakenException($"User with login: {userDto.Login} already exists");
            }

            var userForSaving = _mapper.Map<User>(userDto);
            await _unitOfWork.Users.CreateAsync(userForSaving);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDto>(userForSaving);
        }

        private async Task<bool> DoesUserWithThisLoginExist(string providedLogin) => await _unitOfWork.Users.GetByLoginAsync(providedLogin) != null;
    }
}