using HRMS.Application.Common.Interfaces;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            // Later domain events can be dispatched here

            return await _context.SaveChangesAsync(
                cancellationToken);
        }

        public async Task BeginTransactionAsync(
            CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
                return;

            _transaction =
                await _context.Database
                    .BeginTransactionAsync(
                        cancellationToken);
        }

        public async Task CommitTransactionAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);

                if (_transaction != null)
                {
                    await _transaction.CommitAsync(
                        cancellationToken);

                    await _transaction.DisposeAsync();

                    _transaction = null;
                }
            }
            catch
            {
                await RollbackTransactionAsync(
                    cancellationToken);

                throw;
            }
        }

        public async Task RollbackTransactionAsync(
            CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(
                    cancellationToken);

                await _transaction.DisposeAsync();

                _transaction = null;
            }
        }
    }
}
