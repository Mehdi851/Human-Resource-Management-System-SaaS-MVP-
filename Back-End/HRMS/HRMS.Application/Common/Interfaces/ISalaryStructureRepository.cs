using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface ISalaryStructureRepository
    {
        Task<SalaryStructure?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> HasOverlappingStructureAsync(
            Guid employeeId,
            DateOnly effectiveFrom,
            DateOnly? effectiveTo,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            SalaryStructure salaryStructure,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<SalaryStructure>> GetListAsync(
            Guid organizationId,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<SalaryStructure>> GetEffectiveForEmployeesAsync(
            Guid organizationId,
            DateOnly payrollPeriodStart,
            DateOnly payrollPeriodEnd,
            CancellationToken cancellationToken = default);
    }
}
