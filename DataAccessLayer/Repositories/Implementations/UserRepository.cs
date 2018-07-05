using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccessLayer.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext dbContext) => _db = dbContext;

        public async Task<User> GetByIdAsync(int id) => await _db.Users.FindAsync(id);

        public async Task<User> GetByLoginAsync(string providedLogin) =>
            await _db.Users.FirstOrDefaultAsync(user => string.Equals(user.Login, providedLogin, StringComparison.OrdinalIgnoreCase));

        public async Task<IEnumerable<User>> GetAllAsync() => await _db.Users.ToListAsync();

        public async Task CreateAsync(User user) => await _db.Users.AddAsync(user);
    }
}