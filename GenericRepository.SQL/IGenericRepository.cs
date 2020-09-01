using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository.SQL
{
    public interface IGenericRepository<T> where T : class
    {
        EntityState Add(T entity);

        Task AddAsync(T entity);

        EntityState Update(T entity);

        EntityState Remove(T entity);

        T GetById<TKey>(TKey id);

        Task<T> GetByIdAsync<TKey>(TKey id);

        IQueryable<T> GetAll();

        IQueryable<T> GetAll(int page, int pageCount);

        IQueryable<T> GetAll(string[] includes);

        IQueryable<T> GetAll(int page, int pageCount, string[] includes);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string[] includes);

        IQueryable<T> RawSql(string sql, params object[] parameters);

        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
