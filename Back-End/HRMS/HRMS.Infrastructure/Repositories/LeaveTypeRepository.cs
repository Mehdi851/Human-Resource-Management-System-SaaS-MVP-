using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class LeaveTypeRepository : Repository<LeaveType>, ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveTypeRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> LeaveTypeNameExistsAsync(
            Guid organizationId,
            string leaveTypeName,
            Guid? excludeLeaveTypeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.LeaveTypes
                .AsNoTracking()
                .Where(x =>
                    x.OrganizationId == organizationId &&
                    !x.IsDeleted &&
                    x.Name.ToLower() == leaveTypeName.ToLower());

            if (excludeLeaveTypeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeLeaveTypeId.Value);
            }

            return await query.AnyAsync(cancellationToken);
        }

        public async Task<(IEnumerable<LeaveType> Items, int TotalCount)> GetPagedAsync(
            Guid organizationId,
            int pageNumber,
            int pageSize,
            string? search,
            string? sortBy,
            bool sortDescending,
            CancellationToken cancellationToken = default)
        {
            var query = _context.LeaveTypes
                .AsNoTracking()
                .Where(x =>
                    x.OrganizationId == organizationId &&
                    !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.Name.Contains(search) ||
                    (x.Description != null &&
                     x.Description.Contains(search)));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = sortBy?.ToLower() switch
            {
                "name" => sortDescending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name),

                "defaultdays" => sortDescending
                    ? query.OrderByDescending(x => x.DefaultDays)
                    : query.OrderBy(x => x.DefaultDays),

                "createdat" => sortDescending
                    ? query.OrderByDescending(x => x.CreatedAt)
                    : query.OrderBy(x => x.CreatedAt),

                _ => query.OrderBy(x => x.Name)
            };

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
