using HRMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> FindAsync(
            Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(Guid id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
