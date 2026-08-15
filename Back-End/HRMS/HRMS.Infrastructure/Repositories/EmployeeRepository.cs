using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.Employees.DTOs;
using HRMS.Application.Features.Employees.Queries.GetEmployees;
using HRMS.Domain.Entities;
using HRMS.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Repositories
{
    public class EmployeeRepository
       : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(
            ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(
                    e => e.Email == email &&
                         !e.IsDeleted,
                    cancellationToken);
        }

        public async Task<Employee?> GetByEmployeeNumberAsync(
            string employeeNumber,
            CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(
                    e => e.EmployeeNumber == employeeNumber &&
                         !e.IsDeleted,
                    cancellationToken);
        }

        public async Task<bool> EmailExistsAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AnyAsync(
                    e => e.Email == email &&
                         !e.IsDeleted,
                    cancellationToken);
        }

        public async Task<bool> EmployeeNumberExistsAsync(
            string employeeNumber,
            CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AnyAsync(
                    e => e.EmployeeNumber == employeeNumber &&
                         !e.IsDeleted,
                    cancellationToken);
        }

        public async Task<Employee?> GetEmployeeWithDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Organization)
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(
                    e => e.Id == id &&
                         !e.IsDeleted,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Employee>>
            GetEmployeesWithDetailsAsync(
                CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Organization)
                .Include(e => e.Manager)
                .Where(e => !e.IsDeleted)
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedResponse<EmployeeListItemDto>> GetPagedEmployeesAsync(
     GetEmployeesQuery request,
     CancellationToken cancellationToken = default)
        {
            IQueryable<Employee> query = _context.Employees
                .AsNoTracking()
                .Include(x => x.Department)
                .Include(x => x.Organization)
                .Where(x => !x.IsDeleted);

            //----------------------------------
            // Search
            //----------------------------------

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                    x.FirstName.Contains(search) ||
                    x.LastName.Contains(search) ||
                    x.Email.Contains(search) ||
                    x.EmployeeNumber!.Contains(search));
            }

            //----------------------------------
            // Filters
            //----------------------------------

            if (request.OrganizationId.HasValue)
            {
                query = query.Where(x =>
                    x.OrganizationId == request.OrganizationId);
            }

            if (request.DepartmentId.HasValue)
            {
                query = query.Where(x =>
                    x.DepartmentId == request.DepartmentId);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == request.Status);
            }

            //----------------------------------
            // Sorting
            //----------------------------------

            query = request.SortBy.ToLower() switch
            {
                "lastname" =>
                    request.Descending
                        ? query.OrderByDescending(x => x.LastName)
                        : query.OrderBy(x => x.LastName),

                "email" =>
                    request.Descending
                        ? query.OrderByDescending(x => x.Email)
                        : query.OrderBy(x => x.Email),

                
                _ =>
                    request.Descending
                        ? query.OrderByDescending(x => x.FirstName)
                        : query.OrderBy(x => x.FirstName)
            };

            //----------------------------------
            // Count
            //----------------------------------

            var totalRecords =
                await query.CountAsync(cancellationToken);

            //----------------------------------
            // Paging
            //----------------------------------

            var employees = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new EmployeeListItemDto
                {
                    Id = x.Id,
                    EmployeeNumber = x.EmployeeNumber ?? "",
                    FullName = x.FirstName + " " + x.LastName,
                    Email = x.Email,
                    Department = x.Department.Name,
                    Organization = x.Organization.Name,
                    Status = x.Status.ToString()
                })
                .ToListAsync(cancellationToken);

            return new PagedResponse<EmployeeListItemDto>
            {
                Items = employees,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords
            };
        }

        //    public async Task<PagedResponse<EmployeeListItemDto>> GetPagedEmployeesAsync(
        //int pageNumber,
        //int pageSize,
        //CancellationToken cancellationToken = default)
        //    {
        //        var query = _context.Employees
        //            .AsNoTracking()
        //            .Include(x => x.Department)
        //            .Include(x => x.Organization)
        //            .Where(x => !x.IsDeleted);

        //        var totalRecords = await query.CountAsync(cancellationToken);

        //        var items = await query
        //            .OrderBy(x => x.FirstName)
        //            .Skip((pageNumber - 1) * pageSize)
        //            .Take(pageSize)
        //            .Select(x => new EmployeeListItemDto
        //            {
        //                Id = x.Id,
        //                EmployeeNumber = x.EmployeeNumber ?? "",
        //                FullName = x.FirstName + " " + x.LastName,
        //                Email = x.Email,
        //                Department = x.Department.Name,
        //                Organization = x.Organization.Name,
        //                Status = x.Status.ToString()
        //            })
        //            .ToListAsync(cancellationToken);

        //        return new PagedResponse<EmployeeListItemDto>
        //        {
        //            Items = items,
        //            PageNumber = pageNumber,
        //            PageSize = pageSize,
        //            TotalRecords = totalRecords
        //        };
        //    }
    }
}
