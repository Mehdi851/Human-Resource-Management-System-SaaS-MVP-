using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler
        : IRequestHandler<CreateDepartmentCommand, CreateDepartmentResponse>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentCommandHandler(
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateDepartmentResponse> Handle(
            CreateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            // Business Rule:
            // A department name should be unique within the same organization.
            var departmentExists = await _departmentRepository
                .DepartmentNameExistsAsync(
                    
                    request.OrganizationId,
                    request.Name,
                    null,
                    cancellationToken);

            if (departmentExists)
            {
                throw new ConflictException(
                    "Department with this name already exists.");
            }

            var department = new Department
            {
                OrganizationId = request.OrganizationId,
                Name = request.Name,
                Description = request.Description,
               // ManagerId = request.ManagerId
            };

            await _departmentRepository.AddAsync(department);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new CreateDepartmentResponse
            {
                Id = department.Id
            };
        }
    }
}
