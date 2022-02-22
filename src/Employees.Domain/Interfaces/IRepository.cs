using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Employees.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        public Task<List<TEntity>> ListAsync(int skip,
        int limit,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        CancellationToken cancellationToken = default);

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
         CancellationToken cancellationToken = default);

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate,
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
         CancellationToken cancellationToken = default);

        public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
