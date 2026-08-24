using HRMS.Domain.Entities;
using PayrollEntity = HRMS.Domain.Entities.Payroll;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface IPayrollRepository
    {
        Task<bool> ExistsForPeriodAsync(
            Guid organizationId,
            DateOnly periodStart,
            DateOnly periodEnd,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            PayrollEntity payroll,
            CancellationToken cancellationToken = default);

        Task<Payroll?> GetByIdAsync(
            Guid id,
            Guid organizationId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Payroll>> GetListAsync(
            Guid organizationId,
            CancellationToken cancellationToken = default);

        Task<Payroll?> GetForUpdateAsync(
            Guid id,
            Guid organizationId,
            CancellationToken cancellationToken = default);

        Task<Payroll?> GetByPeriodAsync(
            Guid organizationId,
            DateOnly periodStart,
            DateOnly periodEnd,
            CancellationToken cancellationToken = default);
    }
}
