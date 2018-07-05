using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccessLayer.Repositories.Implementations
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _db;

        public GameRepository(AppDbContext dbContext) => _db = dbContext;

        public async Task<Game> GetByIdAsync(int id) => await _db.Games.FindAsync(id);

        public async Task<Game> GetByTitleAsync(string providedTitle) =>
            await _db.Games.FirstOrDefaultAsync(game => string.Equals(game.Title, providedTitle, StringComparison.OrdinalIgnoreCase));

        public async Task<IEnumerable<Game>> GetAllAsync() => await _db.Games.ToListAsync();

        public async Task CreateAsync(Game game) => await _db.Games.AddAsync(game);
    }
}