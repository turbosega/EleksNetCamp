using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    // TODO: someday in future use Specification 
    public interface IRepository<TEntity, in TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> condition);

        Task CreateAsync(TEntity entity);
    }
}