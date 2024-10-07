using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoodSensingServices.Application.Interfaces;
using MoodSensingServices.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MoodSensingServices.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MSAContext _context;
        private bool disposed = false;
        private readonly string CreationDate = "CreationDate";
        private readonly string ModifiedDate = "ModifiedDate";

        public Repository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<MSAContext>();
        }

        /// <inheritdoc />
        public virtual T? GetById(Guid id)
        {
            using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return _context.Set<T>().Find(id);
            }
        }

        /// <inheritdoc />
        public virtual async Task<T?> Get(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
                .Where(expression)
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
                .Where(expression)
                .ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<List<T>> SelectAll()
        {
            return await _context.Set<T>()
                .ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task SaveAsync()
        {
            var addedEntities = _context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added).ToList();
            addedEntities.ForEach(entry =>
            {
                entry.Property(CreationDate).CurrentValue = DateTime.UtcNow;
                entry.Property(ModifiedDate).CurrentValue = DateTime.UtcNow;
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            var addedEntities = _context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added).ToList();
            addedEntities.ForEach(entry =>
            {
                entry.Property(CreationDate).CurrentValue = DateTime.UtcNow;
                entry.Property(ModifiedDate).CurrentValue = DateTime.UtcNow;
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <inheritdoc />
        public virtual async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the context object
        /// </summary>
        /// <param name="disposing">Disposing flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <inheritdoc />
        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
