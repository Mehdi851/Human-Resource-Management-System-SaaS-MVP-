using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler
        : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDepartmentCommandHandler(
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            DeleteDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id);

            if (department is null || department.IsDeleted)
            {
                throw new NotFoundException("Department not found.");
            }

            // Soft delete instead of removing the record permanently.
            department.IsDeleted = true;
            department.UpdatedAt = DateTime.UtcNow;

            _departmentRepository.Update(department);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
