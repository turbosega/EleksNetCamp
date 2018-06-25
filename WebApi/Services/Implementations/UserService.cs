using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;
using WebApi.UnitsOfWork.Interfaces;

namespace WebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork           _unitOfWork;
        private readonly IMapper               _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork     = unitOfWork;
            _mapper         = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            var userWithThisLoginAlreadyExists = await _unitOfWork.Users.GetByLoginAsync(userDto.Login) != null;
            if (userWithThisLoginAlreadyExists)
            {
                throw new ArgumentException("Login, m'faqa");
            }

            var userForSaving = _mapper.Map<User>(userDto);
            await _unitOfWork.Users.CreateAsync(userForSaving);
            await _unitOfWork.SaveChangesAsync();
            return userForSaving;
        }

        // private methods - provided data validation etc.
    }
}