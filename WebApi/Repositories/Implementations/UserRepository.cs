using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models.Entities;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext dbContext) => _db = dbContext;

        public async Task<User> GetByIdAsync(int id) => await _db.Users.FindAsync(id);

        public async Task<User> GetByLoginAsync(string providedLogin) =>
            await _db.Users
                     .AsNoTracking()
                     .FirstOrDefaultAsync(user => string.Equals(user.Login, providedLogin, StringComparison.OrdinalIgnoreCase));

        public async Task<IEnumerable<User>> GetAllAsync() => await _db.Users.AsNoTracking().ToListAsync();

        public async Task CreateAsync(User newUser) => await _db.Users.AddAsync(newUser);
    }
}