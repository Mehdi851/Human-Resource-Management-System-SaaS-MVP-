using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface IDesignationRepository : IRepository<Designation>
    {
        Task<bool> DesignationNameExistsAsync(
            Guid organizationId,
            string designationName,
            Guid? excludeDesignationId = null,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Designation> Items, int TotalCount)> GetPagedAsync(
            Guid organizationId,
            string? search,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
