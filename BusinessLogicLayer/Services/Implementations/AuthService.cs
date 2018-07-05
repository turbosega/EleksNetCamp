using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.UnitsOfWork.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models.DataTransferObjects;
using Models.Entities;
using Models.Enumerations;

namespace BusinessLogicLayer.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork           _unitOfWork;
        private readonly JwtSettings           _jwtSettings;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtOptions, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork     = unitOfWork;
            _jwtSettings    = jwtOptions.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> AuthenticateAsync(UserDto userDto) =>
            BuildToken(await GetUserIfCorrectCredentialsAsync(userDto.Login, userDto.Password));

        private async Task<User> GetUserIfCorrectCredentialsAsync(string providedLogin, string providedPassword)
        {
            var user = await _unitOfWork.Users.GetByLoginAsync(providedLogin);
            if (user == null || !IsPasswordMatchesWithHash(user, providedPassword))
            {
                throw new BadCredentialsException($"Wrong login: {providedLogin} and/or password: {providedPassword}");
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
                new Claim("login", user.Login),
                new Claim("role", user.UserType == UserType.Administrator ? "admin" : "regular")
            };

            var token = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                             audience: _jwtSettings.Audience,
                                             claims: claims,
                                             notBefore: DateTime.Now,
                                             expires: DateTime.Now.AddDays(7),
                                             signingCredentials: _jwtSettings.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}