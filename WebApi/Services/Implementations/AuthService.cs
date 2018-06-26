using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;
using WebApi.UnitsOfWork.Interfaces;

namespace WebApi.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork           _unitOfWork;
        private readonly IConfiguration        _config;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork     = unitOfWork;
            _config         = configuration;
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
            var jwtSection  = _config.GetSection("JWT");
            var key         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("login", user.Login)
            };
            var token = new JwtSecurityToken(issuer: jwtSection["Issuer"],
                                             audience: jwtSection["Audience"],
                                             claims: claims,
                                             notBefore: DateTime.Now,
                                             expires: DateTime.Now.AddDays(1),
                                             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}