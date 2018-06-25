using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Repositories.Interfaces;
using WebApi.UnitsOfWork.Interfaces;

namespace WebApi.UnitsOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public IUserRepository Users { get; }

        public UnitOfWork(AppDbContext dbContext, IUserRepository userRepository)
        {
            _db   = dbContext;
            Users = userRepository;
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}