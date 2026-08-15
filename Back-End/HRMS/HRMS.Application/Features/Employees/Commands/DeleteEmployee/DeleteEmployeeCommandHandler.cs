using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler
       : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(
            IRepository<Employee> employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteEmployeeResponse> Handle(
            DeleteEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);

            if (employee == null || employee.IsDeleted)
            {
                throw new NotFoundException("Employee not found.");
            }

            _employeeRepository.Delete(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeleteEmployeeResponse
            {
                Id = employee.Id,
                Message = "Employee deleted successfully."
            };
        }
    }
}
