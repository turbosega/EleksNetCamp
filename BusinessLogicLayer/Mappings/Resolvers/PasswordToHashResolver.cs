using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.DataTransferObjects;
using Models.Entities;

namespace BusinessLogicLayer.Mappings.Resolvers
{
    public class PasswordToHashResolver : IValueResolver<UserDto, User, string>
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordToHashResolver(IPasswordHasher<User> passwordHasher) => _passwordHasher = passwordHasher;

        public string Resolve(UserDto source, User destination, string destMember, ResolutionContext context) =>
            _passwordHasher.HashPassword(null, source.Password);
    }
}