using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler
    : IRequestHandler<CreateLeaveTypeCommand, CreateLeaveTypeResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLeaveTypeCommandHandler(
            ILeaveTypeRepository leaveTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateLeaveTypeResponse> Handle(
            CreateLeaveTypeCommand request,
            CancellationToken cancellationToken)
        {
            var leaveTypeName = request.Name.Trim();

            var exists = await _leaveTypeRepository.LeaveTypeNameExistsAsync(
                request.OrganizationId,
                leaveTypeName,
                null,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    "A leave type with the same name already exists in this organization.");
            }

            var leaveType = new LeaveType
            {
                Id = Guid.NewGuid(),
                OrganizationId = request.OrganizationId,
                Name = leaveTypeName,
                Description = string.IsNullOrWhiteSpace(request.Description)
                    ? null
                    : request.Description.Trim(),
                IsPaid = request.IsPaid,
                DefaultDays = request.DefaultDays
            };

            await _leaveTypeRepository.AddAsync(
                leaveType
                );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateLeaveTypeResponse
            {
                Id = leaveType.Id,
                OrganizationId = leaveType.OrganizationId,
                Name = leaveType.Name,
                Description = leaveType.Description,
                IsPaid = leaveType.IsPaid,
                DefaultDays = leaveType.DefaultDays
            };
        }
    }
}
