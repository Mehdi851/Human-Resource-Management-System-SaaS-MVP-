using HRMS.Application.Common.Models;
using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        Task<bool> AttendanceExistsAsync(
            Guid employeeId,
            DateOnly attendanceDate,
            Guid? excludeAttendanceId = null,
            CancellationToken cancellationToken = default);

        Task<Attendance?> GetAttendanceWithDetailsAsync(
            Guid attendanceId,
            Guid organizationId,
            CancellationToken cancellationToken = default);

        Task<PagedResponse<Attendance>> GetPagedAsync(
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
            CancellationToken cancellationToken = default);

        Task<PagedResponse<Attendance>> GetEmployeeAttendanceAsync(
            Guid employeeId,
            Guid organizationId,
            DateOnly? fromDate,
            DateOnly? toDate,
            AttendanceStatus? status,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<PagedResponse<Attendance>> GetAttendanceByDateAsync(
            Guid organizationId,
            DateOnly attendanceDate,
            Guid? departmentId,
            Guid? employeeId,
            AttendanceStatus? status,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
