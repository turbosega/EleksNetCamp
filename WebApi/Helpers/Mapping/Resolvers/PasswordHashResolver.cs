using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Mapping.Resolvers
{
    public class PasswordHashResolver : IValueResolver<UserDto, User, string>
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordHashResolver(IPasswordHasher<User> passwordHasher) => _passwordHasher = passwordHasher;

        public string Resolve(UserDto source, User destination, string destMember, ResolutionContext context) =>
            _passwordHasher.HashPassword(null, source.Password);
    }
}