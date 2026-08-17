using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler
    : IRequestHandler<DeleteLeaveTypeCommand, bool>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveTypeCommandHandler(
            ILeaveTypeRepository leaveTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(
            DeleteLeaveTypeCommand request,
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

            leaveType.IsDeleted = true;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
