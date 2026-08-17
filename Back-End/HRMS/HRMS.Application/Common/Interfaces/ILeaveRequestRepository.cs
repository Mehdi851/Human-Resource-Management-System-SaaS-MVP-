using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
        Task<bool> HasOverlappingLeaveAsync(
            Guid employeeId,
            DateOnly startDate,
            DateOnly endDate,
            Guid? excludeLeaveRequestId = null,
            CancellationToken cancellationToken = default);

        Task<LeaveRequest?> GetByIdWithDetailsAsync(
            Guid id,
            Guid organizationId,
            CancellationToken cancellationToken = default);

        Task<(IEnumerable<LeaveRequest> Items, int TotalCount)> GetPagedAsync(
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
            CancellationToken cancellationToken = default);

        Task<IEnumerable<LeaveRequest>> GetPendingRequestsAsync(
            Guid organizationId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<LeaveRequest>> GetEmployeeLeaveHistoryAsync(
            Guid organizationId,
            Guid employeeId,
            CancellationToken cancellationToken = default);
    }
}
