using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccessLayer.Repositories.Implementations
{
    public class ResultRepository : IResultRepository
    {
        private readonly AppDbContext _db;

        public ResultRepository(AppDbContext dbContext) => _db = dbContext;

        public async Task<Result> GetByIdAsync(int id) => await _db.Results.FindAsync(id);

        public async Task<IEnumerable<Result>> GetAllAsync() => await _db.Results.ToListAsync();

        public async Task CreateAsync(Result result) => await _db.Results.AddAsync(result);
    }
}