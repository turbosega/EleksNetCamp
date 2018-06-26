using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApi.Exceptions;
using WebApi.Helpers;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;
using WebApi.UnitsOfWork.Interfaces;

namespace WebApi.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork           _unitOfWork;
        private readonly JwtSettings           _jwtSettings;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork     = unitOfWork;
            _jwtSettings    = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }


        public async Task<string> AuthenticateAsync(UserDto userDto) =>
            BuildToken(await GetUserIfCorrectCredentialsAsync(userDto.Login, userDto.Password));

        // private methods

        private async Task<User> GetUserIfCorrectCredentialsAsync(string login, string password)
        {
            var user = await _unitOfWork.Users.GetByLoginAsync(login);
            if (user == null || !IsPasswordMatchesWithHash(user, password))
            {
                throw new BadCredentialsException($"Wrong login: {login} and/or password: {password}");
            }

            return user;
        }

        private bool IsPasswordMatchesWithHash(User user, string rawPassword) =>
            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, rawPassword) != PasswordVerificationResult.Failed;

        private string BuildToken(User user)
        {
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("login", user.Login)
            };
            var token = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                             audience: _jwtSettings.Audience,
                                             claims: claims,
                                             notBefore: DateTime.Now,
                                             expires: DateTime.Now.AddDays(1),
                                             signingCredentials: _jwtSettings.SigningCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}