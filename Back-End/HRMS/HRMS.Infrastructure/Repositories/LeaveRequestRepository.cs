using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class LeaveRequestRepository
    : Repository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasOverlappingLeaveAsync(
            Guid employeeId,
            DateOnly startDate,
            DateOnly endDate,
            Guid? excludeLeaveRequestId = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.LeaveRequests
                .AsNoTracking()
                .Where(x =>
                    x.EmployeeId == employeeId &&
                    !x.IsDeleted &&
                    (x.Status == LeaveRequestStatus.Pending ||
                     x.Status == LeaveRequestStatus.Approved) &&
                    x.StartDate <= endDate &&
                    x.EndDate >= startDate);

            if (excludeLeaveRequestId.HasValue)
            {
                query = query.Where(x =>
                    x.Id != excludeLeaveRequestId.Value);
            }

            return await query.AnyAsync(cancellationToken);
        }

        public async Task<LeaveRequest?> GetByIdWithDetailsAsync(
            Guid id,
            Guid organizationId,
            CancellationToken cancellationToken = default)
        {
            return await _context.LeaveRequests
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(
                    x =>
                        x.Id == id &&
                        x.OrganizationId == organizationId &&
                        !x.IsDeleted,
                    cancellationToken);
        }

        public async Task<(IEnumerable<LeaveRequest> Items, int TotalCount)> GetPagedAsync(
            Guid organizationId,
            int pageNumber,
            int pageSize,
            string? search,
            Guid? employeeId,
            Guid? departmentId,
            Guid? leaveTypeId,
            LeaveRequestStatus? status,
            DateOnly? startDate,
            DateOnly? endDate,
            string? sortBy,
            bool sortDescending,
            CancellationToken cancellationToken = default)
        {
            var query = _context.LeaveRequests
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.LeaveType)
                .Where(x =>
                    x.OrganizationId == organizationId &&
                    !x.IsDeleted);

            if (employeeId.HasValue)
            {
                query = query.Where(x =>
                    x.EmployeeId == employeeId.Value);
            }

            if (departmentId.HasValue)
            {
                query = query.Where(x =>
                    x.Employee.DepartmentId == departmentId.Value);
            }

            if (leaveTypeId.HasValue)
            {
                query = query.Where(x =>
                    x.LeaveTypeId == leaveTypeId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == status.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(x =>
                    x.EndDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(x =>
                    x.StartDate <= endDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.Employee.FirstName.Contains(search) ||
                    x.Employee.LastName.Contains(search) ||
                    (x.Reason != null &&
                     x.Reason.Contains(search)) ||
                    x.LeaveType.Name.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = sortBy?.ToLower() switch
            {
                "startdate" => sortDescending
                    ? query.OrderByDescending(x => x.StartDate)
                    : query.OrderBy(x => x.StartDate),

                "enddate" => sortDescending
                    ? query.OrderByDescending(x => x.EndDate)
                    : query.OrderBy(x => x.EndDate),

                "totaldays" => sortDescending
                    ? query.OrderByDescending(x => x.TotalDays)
                    : query.OrderBy(x => x.TotalDays),

                "status" => sortDescending
                    ? query.OrderByDescending(x => x.Status)
                    : query.OrderBy(x => x.Status),

                "createdat" => sortDescending
                    ? query.OrderByDescending(x => x.CreatedAt)
                    : query.OrderBy(x => x.CreatedAt),

                _ => query.OrderByDescending(x => x.CreatedAt)
            };

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<IEnumerable<LeaveRequest>> GetPendingRequestsAsync(
            Guid organizationId,
            CancellationToken cancellationToken = default)
        {
            return await _context.LeaveRequests
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.LeaveType)
                .Where(x =>
                    x.OrganizationId == organizationId &&
                    !x.IsDeleted &&
                    x.Status == LeaveRequestStatus.Pending)
                .OrderBy(x => x.StartDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<LeaveRequest>> GetEmployeeLeaveHistoryAsync(
            Guid organizationId,
            Guid employeeId,
            CancellationToken cancellationToken = default)
        {
            return await _context.LeaveRequests
                .AsNoTracking()
                .Include(x => x.LeaveType)
                .Where(x =>
                    x.OrganizationId == organizationId &&
                    x.EmployeeId == employeeId &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync(cancellationToken);
        }
    }
}
