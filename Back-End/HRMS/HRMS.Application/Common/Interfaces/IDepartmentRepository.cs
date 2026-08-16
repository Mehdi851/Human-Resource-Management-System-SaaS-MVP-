using HRMS.Application.Common.Models;
using HRMS.Application.Features.Departments.DTOs;
using HRMS.Application.Features.Departments.Queries.GetDepartments;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        /// <summary>
        /// Returns a department along with its related entities.
        /// </summary>
        Task<Department?> GetDepartmentWithDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all departments with related entities.
        /// </summary>
        Task<IReadOnlyList<Department>> GetDepartmentsWithDetailsAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns paginated department data.
        /// </summary>
        Task<PagedResponse<DepartmentListItemDto>> GetPagedDepartmentsAsync(
            GetDepartmentsQuery query,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether a department name already exists
        /// within an organization.
        /// </summary>
        Task<bool> DepartmentNameExistsAsync(
             Guid organizationId,
             string departmentName,
             Guid? excludeDepartmentId = null,
             CancellationToken cancellationToken = default);
    }
}
