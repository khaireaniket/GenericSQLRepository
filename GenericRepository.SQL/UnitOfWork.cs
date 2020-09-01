using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GenericRepository.SQL
{
    internal class UnitOfWork<TContext> : IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext, IDisposable
    {
        public TContext Context { get; }

        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories.Add(type, new GenericRepository<T>(Context));
            }

            return (IGenericRepository<T>)_repositories[type];
        }

        public void Commit()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context?.Dispose();
                }
            }
        }
    }
}
