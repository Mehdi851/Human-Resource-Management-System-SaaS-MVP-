using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.ApproveLeaveRequest
{
    public class ApproveLeaveRequestCommandHandler
    : IRequestHandler<ApproveLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            ApproveLeaveRequestCommand request,
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
                    "Only pending leave requests can be approved.");
            }

            var employee =
                await _employeeRepository.GetByIdAsync(
                    leaveRequest.EmployeeId);

            if (employee is null ||
                employee.IsDeleted ||
                employee.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Employee not found.");
            }

            var leaveType =
                await _leaveTypeRepository.GetByIdAsync(
                    leaveRequest.LeaveTypeId);

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
                    leaveRequest.StartDate,
                    leaveRequest.EndDate,
                    leaveRequest.Id,
                    cancellationToken);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "The leave request overlaps with another pending or approved leave request.");
            }

            leaveRequest.Status = LeaveRequestStatus.Approved;

            leaveRequest.ApprovedBy = request.ApprovedBy;

            leaveRequest.ApprovedOn = DateTime.UtcNow;

            leaveRequest.RejectionReason = null;

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
