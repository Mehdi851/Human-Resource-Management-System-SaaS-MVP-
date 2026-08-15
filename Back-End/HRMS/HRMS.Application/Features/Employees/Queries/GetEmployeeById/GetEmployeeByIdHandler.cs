using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdHandler
       : IRequestHandler<
           GetEmployeeByIdQuery,
           GetEmployeeByIdResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdHandler(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<GetEmployeeByIdResponse> Handle(
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken)
        {
            var employee =
                await _employeeRepository
                    .GetEmployeeWithDetailsAsync(request.Id , cancellationToken);

            if (employee is null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }

            return new GetEmployeeByIdResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                EmployeeNumber = employee.EmployeeNumber,
                DepartmentId = (Guid)employee.DepartmentId,
                DepartmentName = employee.Department.Name,
                OrganizationId = employee.OrganizationId,
                OrganizationName = employee.Organization.Name
            };
        }
    }
}
