using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface ILeaveTypeRepository : IRepository<LeaveType>
    {
        Task<bool> LeaveTypeNameExistsAsync(
            Guid organizationId,
            string leaveTypeName,
            Guid? excludeLeaveTypeId = null,
            CancellationToken cancellationToken = default);

        Task<(IEnumerable<LeaveType> Items, int TotalCount)> GetPagedAsync(
            Guid organizationId,
            int pageNumber,
            int pageSize,
            string? search,
            string? sortBy,
            bool sortDescending,
            CancellationToken cancellationToken = default);
    }
}
