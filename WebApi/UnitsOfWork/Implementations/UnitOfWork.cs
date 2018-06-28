using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Repositories.Interfaces;
using WebApi.UnitsOfWork.Interfaces;

namespace WebApi.UnitsOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public IUserRepository  Users  { get; }
        public IGameRepository  Games  { get; }
        public IScoreRepository Scores { get; }

        public UnitOfWork(AppDbContext dbContext,
                          IUserRepository userRepository, IGameRepository gameRepository, IScoreRepository scoreRepository)
        {
            _db    = dbContext;
            Users  = userRepository;
            Games  = gameRepository;
            Scores = scoreRepository;
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}