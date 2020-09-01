using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository.SQL
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public virtual EntityState Add(T entity)
        {
            return _dbSet.Add(entity).State;
        }

        public virtual Task AddAsync(T entity)
        {
            return _dbSet.AddAsync(entity).AsTask();
        }

        public virtual EntityState Update(T entity)
        {
            return _dbSet.Update(entity).State;
        }

        public virtual EntityState Remove(T entity)
        {
            return _dbSet.Remove(entity).State;
        }

        public T GetById<TKey>(TKey id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync<TKey>(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(int page, int pageCount)
        {
            var pageSize = (page - 1) * pageCount;

            return _dbSet.Skip(pageSize).Take(pageCount);
        }

        public IQueryable<T> GetAll(string[] includes)
        {
            var result = GetAll();

            foreach (var include in includes)
            {
                result.Include(include);
            }

            return result;
        }

        public IQueryable<T> GetAll(int page, int pageCount, string[] includes)
        {
            var result = GetAll(page, pageCount);

            foreach (var include in includes)
            {
                result.Include(include);
            }

            return result;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include)
        {
            return FindBy(predicate).Include(include);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string[] includes)
        {
            var result = FindBy(predicate);

            foreach (var include in includes)
            {
                result.Include(include);
            }

            return result;
        }

        public IQueryable<T> RawSql(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
    }
}
