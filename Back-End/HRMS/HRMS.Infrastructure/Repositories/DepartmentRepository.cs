using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.Departments.DTOs;
using HRMS.Application.Features.Departments.Queries.GetDepartments;
using HRMS.Domain.Entities;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class DepartmentRepository
       : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a department with its related entities.
        /// AsNoTracking() improves performance because this
        /// query is used only for reading data.
        /// </summary>
        public async Task<Department?> GetDepartmentWithDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                .AsNoTracking()
                .Include(x => x.Organization)
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted,
                    cancellationToken);
        }

        /// <summary>
        /// Returns all active departments with related data.
        /// </summary>
        public async Task<IReadOnlyList<Department>> GetDepartmentsWithDetailsAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                .AsNoTracking()
                .Include(x => x.Organization)
                .Include(x => x.Employees)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Checks whether another department with the same
        /// name already exists in the organization.
        /// Used during Create and Update validation.
        /// </summary>
        public async Task<bool> DepartmentNameExistsAsync(
            Guid organizationId,
            string departmentName,
            Guid? excludeDepartmentId = null,
            CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                .AnyAsync(x =>
                    x.OrganizationId == organizationId &&
                    x.Name == departmentName &&
                    !x.IsDeleted &&
                    (!excludeDepartmentId.HasValue || x.Id != excludeDepartmentId),
                    cancellationToken);
        }

        /// <summary>
        /// Returns paginated departments with searching
        /// and sorting support.
        /// </summary>
        public async Task<PagedResponse<DepartmentListItemDto>> GetPagedDepartmentsAsync(
            GetDepartmentsQuery request,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Department> query = _context.Departments
                .AsNoTracking()
                .Include(x => x.Organization)
                .Include(x => x.Employees)
                .Where(x => !x.IsDeleted);

            //----------------------------------
            // Search
            //----------------------------------

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                    x.Name.Contains(search) ||
                    (x.Description != null &&
                     x.Description.Contains(search)));
            }

            //----------------------------------
            // Filter
            //----------------------------------

            if (request.OrganizationId.HasValue)
            {
                query = query.Where(x =>
                    x.OrganizationId == request.OrganizationId);
            }

            //----------------------------------
            // Sorting
            //----------------------------------

            query = request.SortBy.ToLower() switch
            {
                "description" =>
                    request.Descending
                        ? query.OrderByDescending(x => x.Description)
                        : query.OrderBy(x => x.Description),

                _ =>
                    request.Descending
                        ? query.OrderByDescending(x => x.Name)
                        : query.OrderBy(x => x.Name)
            };

            //----------------------------------
            // Total Records
            //----------------------------------

            var totalRecords =
                await query.CountAsync(cancellationToken);

            //----------------------------------
            // Paging
            //----------------------------------

            var departments = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DepartmentListItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Organization = x.Organization.Name,
                    EmployeeCount = x.Employees.Count
                })
                .ToListAsync(cancellationToken);

            return new PagedResponse<DepartmentListItemDto>
            {
                Items = departments,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords
            };
        }
    }
}
