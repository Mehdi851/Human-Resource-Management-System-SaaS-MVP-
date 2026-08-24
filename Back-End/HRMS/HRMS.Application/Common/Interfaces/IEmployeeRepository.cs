using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.Employees.DTOs;
using HRMS.Application.Features.Employees.Queries.GetEmployees;
using HRMS.Domain.Entities;

namespace HRMS.Application.Common.Interfaces
{
   
        public interface IEmployeeRepository : IRepository<Employee>
        {
            Task<Employee?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

            Task<Employee?> GetByEmployeeNumberAsync(
                string employeeNumber,
                CancellationToken cancellationToken = default);

            Task<bool> EmailExistsAsync(
                string email,
                CancellationToken cancellationToken = default);

            Task<bool> EmployeeNumberExistsAsync(
                string employeeNumber,
                CancellationToken cancellationToken = default);

            Task<Employee?> GetEmployeeWithDetailsAsync(
                Guid id,
                CancellationToken cancellationToken = default);

            Task<IReadOnlyList<Employee>> GetEmployeesWithDetailsAsync(
                CancellationToken cancellationToken = default);

            //Task<PagedResponse<EmployeeListItemDto>> GetPagedEmployeesAsync(
            //    int pageNumber,
            //    int pageSize,
            //    CancellationToken cancellationToken = default);
            Task<PagedResponse<EmployeeListItemDto>> GetPagedEmployeesAsync(
                GetEmployeesQuery query,
                CancellationToken cancellationToken = default);
            Task<IReadOnlyList<Employee>> GetActiveEmployeesByOrganizationIdAsync(
                Guid organizationId,
                CancellationToken cancellationToken = default);
    }
    }
