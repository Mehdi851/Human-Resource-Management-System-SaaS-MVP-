using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class SalaryStructureRepository : ISalaryStructureRepository
    {
        private readonly ApplicationDbContext _context;

        public SalaryStructureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SalaryStructure?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.SalaryStructures
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<bool> HasOverlappingStructureAsync(
             Guid employeeId,
             DateOnly effectiveFrom,
             DateOnly? effectiveTo,
             Guid? excludeId = null,
             CancellationToken cancellationToken = default)
        {
            var query = _context.SalaryStructures
                .AsNoTracking()
                .Where(x =>
                    x.EmployeeId == employeeId &&
                    x.Status == Domain.Enums.SalaryStructureStatus.Active);

            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }

            var structures = await query.ToListAsync(cancellationToken);

            foreach (var structure in structures)
            {
                var existingEnd =
                    structure.EffectiveTo ?? DateOnly.MaxValue;

                var newEnd =
                    effectiveTo ?? DateOnly.MaxValue;

                if (effectiveFrom <= existingEnd &&
                    structure.EffectiveFrom <= newEnd)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task AddAsync(
            SalaryStructure salaryStructure,
            CancellationToken cancellationToken = default)
        {
            await _context.SalaryStructures.AddAsync(
                salaryStructure,
                cancellationToken);
        }

        public async Task<IReadOnlyList<SalaryStructure>> GetListAsync(
     Guid organizationId,
     Guid? employeeId = null,
     CancellationToken cancellationToken = default)
        {
            var query = _context.SalaryStructures
                .AsNoTracking()
                .Where(x => x.OrganizationId == organizationId);

            if (employeeId.HasValue)
            {
                query = query.Where(x =>
                    x.EmployeeId == employeeId.Value);
            }

            return await query
                .OrderByDescending(x => x.EffectiveFrom)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<SalaryStructure>>
    GetEffectiveForEmployeesAsync(
        Guid organizationId,
        DateOnly payrollPeriodStart,
        DateOnly payrollPeriodEnd,
        CancellationToken cancellationToken = default)
        {
            return await _context.SalaryStructures
                .AsNoTracking()
                .Where(x =>
                    x.OrganizationId == organizationId &&
                    x.Status == Domain.Enums.SalaryStructureStatus.Active &&

                    // Salary starts before or during payroll period
                    x.EffectiveFrom <= payrollPeriodEnd &&

                    // Salary has no end date OR ends after/before payroll period
                    (!x.EffectiveTo.HasValue ||
                     x.EffectiveTo.Value >= payrollPeriodStart))
                .OrderBy(x => x.EmployeeId)
                .ThenByDescending(x => x.EffectiveFrom)
                .ToListAsync(cancellationToken);
        }


    }
}
