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
                x => x.Email == request.Email);

            if (existingEmployees.Count > 0)
            {
                throw new BadRequestException("Employee with this email already exists.");
            }

            // Entity Creation
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DepartmentId = request.DepartmentId,
                DesignationId = request.DesignationId,
                OrganizationId = request.OrganizationId
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
