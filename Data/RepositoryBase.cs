using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodingExercise.Data
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private readonly ApplicationDbContext _dataContext;
        private readonly DbSet<T> _dbSet;

        protected ApplicationDbContext DbContext
        {
            get { return _dataContext; }
        }
        #endregion

        protected RepositoryBase(ApplicationDbContext context)
        {
            _dataContext = context;
            _dbSet = DbContext.Set<T>();
        }

        #region Implementation

        public virtual void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            _dataContext.SaveChanges();
        }

        public virtual Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            return _dataContext.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
            await _dataContext.SaveChangesAsync();

        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Remove(entity);
            _dataContext.SaveChanges();
        }

        public virtual Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Remove(entity);
            return _dataContext.SaveChangesAsync();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
            _dataContext.SaveChanges();
        }

        public virtual Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
            return _dataContext.SaveChangesAsync();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where);
        }
        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault<T>();
        }

        #endregion

    }
}
