using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Utilities;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Models.DataTransferObjects;
using Models.Entities;

namespace BusinessLogicLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper        _mapper;
        private readonly IUnitOfWork    _unitOfWork;
        private readonly IImageUploader _imageUploader;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IImageUploader imageUploader)
        {
            _mapper        = mapper;
            _unitOfWork    = unitOfWork;
            _imageUploader = imageUploader;
        }

        public async Task<User> GetByIdAsync(int id) => await _unitOfWork.Users.GetByIdAsync(id) ??
                                                        throw new ResourceNotFoundException($"User with {nameof(id)}: {id} not found");

        public async Task<IEnumerable<User>> GetAllAsync() => await _unitOfWork.Users.GetAllAsync();

        public async Task<User> CreateAsync(UserDto userDto)
        {
            if (await DoesUserWithThisLoginExist(userDto.Login))
            {
                throw new LoginIsTakenException($"User with login: {userDto.Login} already exists");
            }

            var userForSaving = MapFromDtoToUser(userDto);
            userForSaving.AvatarUrl = _imageUploader.UploadImageFromForm(userDto.Avatar);
            await _unitOfWork.Users.CreateAsync(userForSaving);
            await _unitOfWork.SaveChangesAsync();
            return userForSaving;
        }

        private async Task<bool> DoesUserWithThisLoginExist(string providedLogin) => await _unitOfWork.Users.GetByLoginAsync(providedLogin) != null;

        private User MapFromDtoToUser(UserDto userDto) => _mapper.Map<User>(userDto);
    }
}