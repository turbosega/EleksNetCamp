using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccessLayer.Repositories.Implementations
{
    public abstract class AbstractRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly AppDbContext Db;

        protected AbstractRepository(AppDbContext dbContext) => Db = dbContext;

        public async Task<TEntity> GetByIdAsync(TKey id) => await Db.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await Db.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> condition) =>
            await Db.Set<TEntity>().AsQueryable().Where(condition).ToListAsync();

        public async Task CreateAsync(TEntity entity) => await Db.Set<TEntity>().AddAsync(entity);
    }
}