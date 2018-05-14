using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodingExercise.Data
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
