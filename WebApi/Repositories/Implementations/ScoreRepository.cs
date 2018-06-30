using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models.Entities;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories.Implementations
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly AppDbContext _db;

        public ScoreRepository(AppDbContext dbContext) => _db = dbContext;

        public async Task<Score> GetByIdAsync(int id) => await _db.Scores.FindAsync(id);

        public async Task<IEnumerable<Score>> GetAllAsync() => await _db.Scores.ToListAsync();

        public async Task CreateAsync(Score score) => await _db.Scores.AddAsync(score);
    }
}