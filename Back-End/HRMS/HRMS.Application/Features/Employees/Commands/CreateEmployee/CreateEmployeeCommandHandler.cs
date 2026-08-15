using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(
            IRepository<Employee> employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateEmployeeResponse> Handle(
            CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            // Business Rule Check
            var existingEmployees = await _employeeRepository.FindAsync(
                x => x.Email == request.Employee.Email);

            if (existingEmployees.Count > 0)
            {
                throw new ConflictException("Employee with this email already exists.");
            }

            // Entity Creation
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = request.Employee.FirstName,
                LastName = request.Employee.LastName,
                Email = request.Employee.Email,
                PhoneNumber = request.Employee.EmployeeNumber,
                DepartmentId = request.Employee.DepartmentId,
                DesignationId = request.Employee.DesignationId,
                OrganizationId = request.Employee.OrganizationId
            };

            // Persistence
            await _employeeRepository.AddAsync(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Response
            return new CreateEmployeeResponse
            {
                Id = employee.Id
            };
        }
    }
}
