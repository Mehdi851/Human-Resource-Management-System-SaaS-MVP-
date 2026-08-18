using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler
    : IRequestHandler<
        UpdateLeaveRequestCommand,
        UpdateLeaveRequestResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateLeaveRequestResponse> Handle(
            UpdateLeaveRequestCommand request,
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
                    "Only pending leave requests can be updated.");
            }

            var leaveType =
                await _leaveTypeRepository.GetByIdAsync(
                    request.LeaveTypeId);

            if (leaveType is null ||
                leaveType.IsDeleted ||
                leaveType.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Leave type not found.");
            }

            var hasOverlap =
                await _leaveRequestRepository.HasOverlappingLeaveAsync(
                    leaveRequest.EmployeeId,
                    request.StartDate,
                    request.EndDate,
                    leaveRequest.Id,
                    cancellationToken);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "The requested leave dates overlap with an existing pending or approved leave request.");
            }

            var totalDays =
                request.EndDate.DayNumber -
                request.StartDate.DayNumber +
                1;

            leaveRequest.LeaveTypeId = request.LeaveTypeId;

            leaveRequest.StartDate = request.StartDate;

            leaveRequest.EndDate = request.EndDate;

            leaveRequest.TotalDays = totalDays;

            leaveRequest.Reason =
                string.IsNullOrWhiteSpace(request.Reason)
                    ? null
                    : request.Reason.Trim();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new UpdateLeaveRequestResponse
            {
                Id = leaveRequest.Id,

                OrganizationId = leaveRequest.OrganizationId,

                EmployeeId = leaveRequest.EmployeeId,

                LeaveTypeId = leaveRequest.LeaveTypeId,

                StartDate = leaveRequest.StartDate,

                EndDate = leaveRequest.EndDate,

                TotalDays = (int)leaveRequest.TotalDays,

                Reason = leaveRequest.Reason,

                Status = leaveRequest.Status.ToString()
            };
        }
    }
}
