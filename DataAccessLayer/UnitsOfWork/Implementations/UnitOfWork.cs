using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;
using DataAccessLayer.UnitsOfWork.Interfaces;

namespace DataAccessLayer.UnitsOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public IUserRepository   Users   { get; }
        public IGameRepository   Games   { get; }
        public IResultRepository Results { get; }

        public UnitOfWork(AppDbContext dbContext,
                          IUserRepository userRepository, IGameRepository gameRepository, IResultRepository resultRepository)
        {
            _db = dbContext;

            Users   = userRepository;
            Games   = gameRepository;
            Results = resultRepository;
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}