using DataAccessLayer.Repositories.Interfaces;
using Models.Entities;

namespace DataAccessLayer.Repositories.Implementations
{
    public class ResultRepository : AbstractRepository<Result, int>, IResultRepository
    {
        public ResultRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}