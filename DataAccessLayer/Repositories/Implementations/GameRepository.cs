using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccessLayer.Repositories.Implementations
{
    public class GameRepository : AbstractRepository<Game, int>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Game> GetByTitleAsync(string providedTitle) =>
            await Db.Games.AsQueryable().SingleOrDefaultAsync(game => string.Equals(game.Title, providedTitle, StringComparison.OrdinalIgnoreCase));
    }
}