using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler
     : IRequestHandler<UpdateLeaveTypeCommand, UpdateLeaveTypeResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLeaveTypeCommandHandler(
            ILeaveTypeRepository leaveTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateLeaveTypeResponse> Handle(
            UpdateLeaveTypeCommand request,
            CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(
                request.Id);

            if (leaveType is null ||
                leaveType.IsDeleted ||
                leaveType.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Leave type not found.");
            }

            var leaveTypeName = request.Name.Trim();

            var exists = await _leaveTypeRepository.LeaveTypeNameExistsAsync(
                request.OrganizationId,
                leaveTypeName,
                request.Id,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    "A leave type with the same name already exists in this organization.");
            }

            leaveType.Name = leaveTypeName;

            leaveType.Description = string.IsNullOrWhiteSpace(request.Description)
                ? null
                : request.Description.Trim();

            leaveType.IsPaid = request.IsPaid;

            leaveType.DefaultDays = request.DefaultDays;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateLeaveTypeResponse
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
