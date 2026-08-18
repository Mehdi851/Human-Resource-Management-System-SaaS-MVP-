using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler
    : IRequestHandler<CancelLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository,
            IUnitOfWork unitOfWork)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            CancelLeaveRequestCommand request,
            CancellationToken cancellationToken)
        {
            var leaveRequest =
                await _leaveRequestRepository.GetByIdAsync(
                    request.Id);

            if (leaveRequest is null ||
                leaveRequest.IsDeleted ||
                leaveRequest.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Leave request not found.");
            }

            if (leaveRequest.Status != LeaveRequestStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Only pending leave requests can be cancelled.");
            }

            leaveRequest.Status = LeaveRequestStatus.Cancelled;

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
