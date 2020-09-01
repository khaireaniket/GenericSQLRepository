using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GenericRepository.SQL
{
    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }

    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task<int> CommitAsync();

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
