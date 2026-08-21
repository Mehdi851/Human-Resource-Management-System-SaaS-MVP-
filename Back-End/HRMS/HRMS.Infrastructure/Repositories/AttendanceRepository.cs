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
    public class AttendanceRepository
     : Repository<Attendance>, IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> AttendanceExistsAsync(
            Guid employeeId,
            DateOnly attendanceDate,
            Guid? excludeAttendanceId = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Attendances
                .AsNoTracking()
                .Where(a =>
                    !a.IsDeleted &&
                    a.EmployeeId == employeeId &&
                    a.AttendanceDate == attendanceDate);

            if (excludeAttendanceId.HasValue)
            {
                query = query.Where(
                    a => a.Id != excludeAttendanceId.Value);
            }

            return await query.AnyAsync(cancellationToken);
        }

        public async Task<Attendance?> GetAttendanceWithDetailsAsync(
            Guid attendanceId,
            Guid organizationId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Attendances
                .AsNoTracking()
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Department)
                .FirstOrDefaultAsync(
                    a =>
                        a.Id == attendanceId &&
                        a.OrganizationId == organizationId &&
                        !a.IsDeleted,
                    cancellationToken);
        }

        public async Task<PagedResponse<Attendance>> GetPagedAsync(
            Guid organizationId,
            Guid? employeeId,
            Guid? departmentId,
            AttendanceStatus? status,
            DateOnly? attendanceDate,
            DateOnly? fromDate,
            DateOnly? toDate,
            string? search,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = BuildBaseQuery(organizationId);

            if (employeeId.HasValue)
            {
                query = query.Where(
                    a => a.EmployeeId == employeeId.Value);
            }

            if (departmentId.HasValue)
            {
                query = query.Where(
                    a => a.Employee.DepartmentId == departmentId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(
                    a => a.Status == status.Value);
            }

            if (attendanceDate.HasValue)
            {
                query = query.Where(
                    a => a.AttendanceDate == attendanceDate.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(
                    a => a.AttendanceDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(
                    a => a.AttendanceDate <= toDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim();

                query = query.Where(a =>
                    a.Employee.FirstName.Contains(searchTerm) ||
                    a.Employee.LastName.Contains(searchTerm) ||
                    (a.Employee.EmployeeNumber != null &&
                     a.Employee.EmployeeNumber.Contains(searchTerm)));
            }

            query = ApplySorting(
                query,
                sortBy,
                sortDescending);

            return await CreatePagedResponseAsync(
                query,
                pageNumber,
                pageSize,
                cancellationToken);
        }

        public async Task<PagedResponse<Attendance>> GetEmployeeAttendanceAsync(
            Guid employeeId,
            Guid organizationId,
            DateOnly? fromDate,
            DateOnly? toDate,
            AttendanceStatus? status,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = BuildBaseQuery(organizationId)
                .Where(a => a.EmployeeId == employeeId);

            if (fromDate.HasValue)
            {
                query = query.Where(
                    a => a.AttendanceDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(
                    a => a.AttendanceDate <= toDate.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(
                    a => a.Status == status.Value);
            }

            query = ApplySorting(
                query,
                sortBy,
                sortDescending);

            return await CreatePagedResponseAsync(
                query,
                pageNumber,
                pageSize,
                cancellationToken);
        }

        public async Task<PagedResponse<Attendance>> GetAttendanceByDateAsync(
            Guid organizationId,
            DateOnly attendanceDate,
            Guid? departmentId,
            Guid? employeeId,
            AttendanceStatus? status,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = BuildBaseQuery(organizationId)
                .Where(a =>
                    a.AttendanceDate == attendanceDate);

            if (departmentId.HasValue)
            {
                query = query.Where(
                    a => a.Employee.DepartmentId == departmentId.Value);
            }

            if (employeeId.HasValue)
            {
                query = query.Where(
                    a => a.EmployeeId == employeeId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(
                    a => a.Status == status.Value);
            }

            query = ApplySorting(
                query,
                sortBy,
                sortDescending);

            return await CreatePagedResponseAsync(
                query,
                pageNumber,
                pageSize,
                cancellationToken);
        }

        private IQueryable<Attendance> BuildBaseQuery(
            Guid organizationId)
        {
            return _context.Attendances
                .AsNoTracking()
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Department)
                .Where(a =>
                    !a.IsDeleted &&
                    a.OrganizationId == organizationId);
        }

        private static IQueryable<Attendance> ApplySorting(
            IQueryable<Attendance> query,
            string? sortBy,
            bool sortDescending)
        {
            var field = sortBy?.Trim().ToLowerInvariant();

            return field switch
            {
                "employeename" => sortDescending
                    ? query.OrderByDescending(
                        a => a.Employee.FirstName)
                    : query.OrderBy(
                        a => a.Employee.FirstName),

                "employeenumber" => sortDescending
                    ? query.OrderByDescending(
                        a => a.Employee.EmployeeNumber)
                    : query.OrderBy(
                        a => a.Employee.EmployeeNumber),

                "status" => sortDescending
                    ? query.OrderByDescending(
                        a => a.Status)
                    : query.OrderBy(
                        a => a.Status),

                "checkin" => sortDescending
                    ? query.OrderByDescending(
                        a => a.CheckInTime)
                    : query.OrderBy(
                        a => a.CheckInTime),

                "checkout" => sortDescending
                    ? query.OrderByDescending(
                        a => a.CheckOutTime)
                    : query.OrderBy(
                        a => a.CheckOutTime),

                "workinghours" => sortDescending
                    ? query.OrderByDescending(
                        a => a.WorkingHours)
                    : query.OrderBy(
                        a => a.WorkingHours),

                _ => sortDescending
                    ? query.OrderByDescending(
                        a => a.AttendanceDate)
                    : query.OrderBy(
                        a => a.AttendanceDate)
            };
        }

        private static async Task<PagedResponse<Attendance>>
            CreatePagedResponseAsync(
                IQueryable<Attendance> query,
                int pageNumber,
                int pageSize,
                CancellationToken cancellationToken)
        {
            var totalCount = await query.CountAsync(
                cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResponse<Attendance>
            {
                Items = items,
                TotalRecords = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
