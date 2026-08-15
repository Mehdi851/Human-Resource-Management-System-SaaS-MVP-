using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler
       : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateEmployeeResponse> Handle(
            UpdateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);

            if (employee is null || employee.IsDeleted)
                throw new NotFoundException("Employee not found.");

            // Only check for duplicate email if it changed
            if (!employee.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailExists = await _employeeRepository.EmailExistsAsync(
                       request.Email,
                       cancellationToken);

                            if (emailExists)
                            {
                                throw new ConflictException("An employee with this email already exists.");
                            }
            }
            if (!string.IsNullOrWhiteSpace(request.EmployeeNumber))
            {
                var employeeNumberExists =
                    await _employeeRepository.EmployeeNumberExistsAsync(
                        request.EmployeeNumber,
                        cancellationToken);

                if (employeeNumberExists)
                {
                    throw new InvalidOperationException(
                        "An employee with this employee number already exists.");
                }
            }

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Email = request.Email;
            employee.EmployeeNumber = request.EmployeeNumber;
            employee.DepartmentId = request.DepartmentId;
            employee.OrganizationId = request.OrganizationId;

            _employeeRepository.Update(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateEmployeeResponse
            {
                Id = employee.Id
            };
        }
    }
}
