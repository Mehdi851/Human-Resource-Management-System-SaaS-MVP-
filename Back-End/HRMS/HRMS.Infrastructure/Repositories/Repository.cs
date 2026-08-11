using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Common;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
     where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Extension hook for specialized repositories.
        /// Allows derived repositories to build custom queries.
        /// </summary>
        protected IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> FindAsync(
            Expression<Func<T, bool>> predicate)
        {
            return await Query()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await Query()
                .AnyAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            // Soft Delete
            entity.IsDeleted = true;

            _dbSet.Update(entity);
        }
    }
}
