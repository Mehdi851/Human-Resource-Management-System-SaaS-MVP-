using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using PayrollEntity = HRMS.Domain.Entities.Payroll;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly ApplicationDbContext _context;

        public PayrollRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsForPeriodAsync(
            Guid organizationId,
            DateOnly periodStart,
            DateOnly periodEnd,
            CancellationToken cancellationToken = default)
        {
            return await _context.Payrolls
                .AsNoTracking()
                .AnyAsync(
                    x =>
                        x.OrganizationId == organizationId &&
                        x.PayrollPeriodStart == periodStart &&
                        x.PayrollPeriodEnd == periodEnd &&
                        x.Status != Domain.Enums.PayrollStatus.Cancelled,
                    cancellationToken);
        }

        public async Task AddAsync(
            PayrollEntity payroll,
            CancellationToken cancellationToken = default)
        {
            await _context.Payrolls.AddAsync(
                payroll,
                cancellationToken);
        }

        public async Task<Payroll?> GetByIdAsync(
            Guid id,
            Guid organizationId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Payrolls
                .AsNoTracking()
                .Include(x => x.PayrollItems)
                .FirstOrDefaultAsync(
                    x =>
                        x.Id == id &&
                        x.OrganizationId == organizationId,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Payroll>> GetListAsync(
            Guid organizationId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Payrolls
                .AsNoTracking()
                .Where(x => x.OrganizationId == organizationId)
                .Include(x => x.PayrollItems)
                .OrderByDescending(x => x.PayrollPeriodStart)
                .ToListAsync(cancellationToken);
        }
        public async Task<Payroll?> GetForUpdateAsync(
            Guid id,
            Guid organizationId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Payrolls
                .FirstOrDefaultAsync(
                    x =>
                        x.Id == id &&
                        x.OrganizationId == organizationId,
                    cancellationToken);
        }

        public async Task<Payroll?> GetByPeriodAsync(
            Guid organizationId,
            DateOnly periodStart,
            DateOnly periodEnd,
            CancellationToken cancellationToken = default)
        {
            return await _context.Payrolls
                .AsNoTracking()
                .Include(x => x.PayrollItems)
                .FirstOrDefaultAsync(
                    x =>
                        x.OrganizationId == organizationId &&
                        x.PayrollPeriodStart == periodStart &&
                        x.PayrollPeriodEnd == periodEnd,
                    cancellationToken);
        }
    }
}
