using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.Departments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryHandler
        : IRequestHandler<GetDepartmentByIdQuery, DepartmentDetailsDto>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentByIdQueryHandler(
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDetailsDto> Handle(
            GetDepartmentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var department = await _departmentRepository
                .GetDepartmentWithDetailsAsync(
                    request.Id,
                    cancellationToken);

            if (department is null)
            {
                throw new NotFoundException("Department not found.");
            }

            return new DepartmentDetailsDto
            {
                Id = department.Id,
                OrganizationId = department.OrganizationId,
                Organization = department.Organization.Name,
                Name = department.Name,
                Description = department.Description,
                //ManagerId = department.ManagerId,
                EmployeeCount = department.Employees.Count
            };
        }
    }
}
