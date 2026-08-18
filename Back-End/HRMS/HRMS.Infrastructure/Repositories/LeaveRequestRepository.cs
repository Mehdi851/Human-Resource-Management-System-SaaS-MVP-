using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
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

        public async Task<LeaveRequest?> GetDetailsByIdAsync(
            Guid id,
            Guid organizationId,
            CancellationToken cancellationToken = default)
                {
                    return await _context.LeaveRequests
                        .AsNoTracking()
                        .Include(x => x.Employee)
                            .ThenInclude(x => x.Department)
                        .Include(x => x.LeaveType)
                        .FirstOrDefaultAsync(
                            x =>
                                x.Id == id &&
                                x.OrganizationId == organizationId &&
                                !x.IsDeleted,
                            cancellationToken);
                }

        public async Task<PagedResponse<LeaveRequest>> GetPagedAsync(
    Guid organizationId,
    Guid? employeeId = null,
    Guid? departmentId = null,
    string? status = null,
    string? search = null,
    DateOnly? startDate = null,
    DateOnly? endDate = null,
    int pageNumber = 1,
    int pageSize = 10,
    string? sortBy = null,
    bool sortDescending = false,
    CancellationToken cancellationToken = default)
        {
            var query = _context.LeaveRequests
                .AsNoTracking()
                .Include(x => x.Employee)
                    .ThenInclude(x => x.Department)
                .Include(x => x.LeaveType)
                .Where(x =>
                    x.OrganizationId == organizationId &&
                    !x.IsDeleted)
                .AsQueryable();

            // Employee filtering
            if (employeeId.HasValue)
            {
                query = query.Where(x =>
                    x.EmployeeId == employeeId.Value);
            }

            // Department filtering
            if (departmentId.HasValue)
            {
                query = query.Where(x =>
                    x.Employee.DepartmentId == departmentId.Value);
            }

            // Status filtering
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (Enum.TryParse<LeaveRequestStatus>(
                    status,
                    true,
                    out var leaveRequestStatus))
                {
                    query = query.Where(x =>
                        x.Status == leaveRequestStatus);
                }
            }

            // Search by employee name, employee number, leave type, or reason
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.Employee.FirstName.Contains(search) ||
                    x.Employee.LastName.Contains(search) ||
                    (x.Employee.EmployeeNumber != null &&
                     x.Employee.EmployeeNumber.Contains(search)) ||
                    x.LeaveType.Name.Contains(search) ||
                    (x.Reason != null &&
                     x.Reason.Contains(search)));
            }

            // Date range filtering
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

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "employee" => sortDescending
                    ? query.OrderByDescending(x => x.Employee.LastName)
                    : query.OrderBy(x => x.Employee.LastName),

                "leavetype" => sortDescending
                    ? query.OrderByDescending(x => x.LeaveType.Name)
                    : query.OrderBy(x => x.LeaveType.Name),

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

                _ => query.OrderByDescending(x => x.StartDate)
            };

            var totalCount = await query.CountAsync(
                cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResponse<LeaveRequest>
            {
                Items = items,

                PageNumber = pageNumber,

                PageSize = pageSize,

                //TotalCount = totalCount
            };
        }
    }
}
