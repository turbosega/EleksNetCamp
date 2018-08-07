using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Utilities.Settings;
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

        public async Task<string> AuthenticateAsync(UserAuthDto authDto) =>
            BuildToken(await GetUserIfCorrectCredentialsAsync(authDto.Login, authDto.Password));

        private async Task<User> GetUserIfCorrectCredentialsAsync(string providedLogin, string providedPassword)
        {
            var user = await _unitOfWork.Users.GetByLoginAsync(providedLogin);
            if (user == null || !DoesPasswordMatchWithHash(user, providedPassword))
            {
                throw new BadCredentialsException($"Wrong login: {providedLogin} and/or password: {providedPassword}");
            }

            return user;
        }

        private bool DoesPasswordMatchWithHash(User user, string rawPassword) =>
            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, rawPassword) != PasswordVerificationResult.Failed;

        private string BuildToken(User user)
        {
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("login", user.Login),
                new Claim(ClaimTypes.Role, user.UserType == UserType.Administrator ? "admin" : "regular")
            };

            var token = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                             audience: _jwtSettings.Audience,
                                             claims: claims,
                                             notBefore: DateTime.Now,
                                             expires: DateTime.Now.AddHours(24),
                                             signingCredentials: _jwtSettings.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}