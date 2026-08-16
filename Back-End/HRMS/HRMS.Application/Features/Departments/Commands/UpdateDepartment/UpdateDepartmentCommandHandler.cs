using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler
        : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDepartmentCommandHandler(
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            UpdateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id);

            if (department is null || department.IsDeleted)
            {
                throw new NotFoundException("Department not found.");
            }

            // Prevent duplicate department names within the same organization.
            var duplicateExists = await _departmentRepository
                    .DepartmentNameExistsAsync(
                        request.OrganizationId,
                        request.Name,
                        request.Id,
                        cancellationToken);

            if (duplicateExists &&
                !department.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
            {
                throw new ConflictException(
                    "Department with this name already exists.");
            }

            department.Name = request.Name;
            department.Description = request.Description;
            //department.ManagerId = request.ManagerId;
            department.UpdatedAt = DateTime.UtcNow;

            _departmentRepository.Update(department);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
