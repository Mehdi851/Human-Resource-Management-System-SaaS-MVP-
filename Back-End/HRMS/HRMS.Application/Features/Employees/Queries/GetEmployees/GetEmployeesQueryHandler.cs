using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.Employees.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler
        : IRequestHandler<GetEmployeesQuery, PagedResponse<EmployeeListItemDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesQueryHandler(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<PagedResponse<EmployeeListItemDto>> Handle(
            GetEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            return await _employeeRepository
                .GetPagedEmployeesAsync(request, cancellationToken);
        }
    }
}
