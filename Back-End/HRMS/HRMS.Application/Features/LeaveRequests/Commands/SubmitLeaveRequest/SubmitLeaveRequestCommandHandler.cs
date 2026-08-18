using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Commands.SubmitLeaveRequest
{
    public class SubmitLeaveRequestCommandHandler
     : IRequestHandler<
         SubmitLeaveRequestCommand,
         SubmitLeaveRequestResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitLeaveRequestCommandHandler(
            IEmployeeRepository employeeRepository,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubmitLeaveRequestResponse> Handle(
            SubmitLeaveRequestCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(
                request.EmployeeId);

            if (employee is null ||
                employee.IsDeleted ||
                employee.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Employee not found.");
            }

            var leaveType = await _leaveTypeRepository.GetByIdAsync(
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
                    request.EmployeeId,
                    request.StartDate,
                    request.EndDate);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "The requested leave dates overlap with an existing pending or approved leave request.");
            }

            var totalDays =
                request.EndDate.DayNumber -
                request.StartDate.DayNumber +
                1;

            var leaveRequest = new LeaveRequest
            {
                Id = Guid.NewGuid(),

                OrganizationId = request.OrganizationId,

                EmployeeId = request.EmployeeId,

                LeaveTypeId = request.LeaveTypeId,

                StartDate = request.StartDate,

                EndDate = request.EndDate,

                TotalDays = totalDays,

                Reason = string.IsNullOrWhiteSpace(request.Reason)
                    ? null
                    : request.Reason.Trim(),

                Status = LeaveRequestStatus.Pending,

                ApprovedBy = null,

                ApprovedOn = null,

                RejectionReason = null
            };

            await _leaveRequestRepository.AddAsync(
                leaveRequest);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new SubmitLeaveRequestResponse
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
