using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccessLayer.Repositories.Implementations
{
    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByLoginAsync(string providedLogin) =>
            await Db.Users.AsQueryable().SingleOrDefaultAsync(user => string.Equals(user.Login, providedLogin, StringComparison.OrdinalIgnoreCase));
    }
}