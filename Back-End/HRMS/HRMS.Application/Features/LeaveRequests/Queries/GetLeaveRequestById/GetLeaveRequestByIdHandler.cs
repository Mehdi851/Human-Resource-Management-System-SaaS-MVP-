using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Queries.GetLeaveRequestById
{
    public class GetLeaveRequestByIdHandler
     : IRequestHandler<
         GetLeaveRequestByIdQuery,
         LeaveRequestDetailsDto?>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestByIdHandler(
            ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<LeaveRequestDetailsDto?> Handle(
            GetLeaveRequestByIdQuery request,
            CancellationToken cancellationToken)
        {
            var leaveRequest =
                await _leaveRequestRepository.GetDetailsByIdAsync(
                    request.Id,
                    request.OrganizationId,
                    cancellationToken);

            if (leaveRequest is null)
            {
                return null;
            }

            return new LeaveRequestDetailsDto
            {
                Id = leaveRequest.Id,

                OrganizationId = leaveRequest.OrganizationId,

                EmployeeId = leaveRequest.EmployeeId,

                EmployeeName =
                    $"{leaveRequest.Employee.FirstName} {leaveRequest.Employee.LastName}",

                EmployeeNumber =
                    leaveRequest.Employee.EmployeeNumber,

                DepartmentId =
                    leaveRequest.Employee.DepartmentId,

                DepartmentName =
                    leaveRequest.Employee.Department?.Name,

                LeaveTypeId =
                    leaveRequest.LeaveTypeId,

                LeaveTypeName =
                    leaveRequest.LeaveType.Name,

                StartDate =
                    leaveRequest.StartDate,

                EndDate =
                    leaveRequest.EndDate,

                TotalDays =
                    (int)leaveRequest.TotalDays,

                Reason =
                    leaveRequest.Reason,

                Status =
                    leaveRequest.Status.ToString(),

                ApprovedBy =
                    leaveRequest.ApprovedBy,

                ApprovedOn =
                    leaveRequest.ApprovedOn,

                RejectionReason =
                    leaveRequest.RejectionReason
            };
        }
    }
}
