using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.LeaveRequests.Queries.GetLeaveRequests;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Queries.GetPendingLeaveRequests
{
    public class GetPendingLeaveRequestsHandler
    : IRequestHandler<
        GetPendingLeaveRequestsQuery,
        PagedResponse<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository
            _leaveRequestRepository;

        public GetPendingLeaveRequestsHandler(
            ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository =
                leaveRequestRepository;
        }

        public async Task<PagedResponse<LeaveRequestListDto>> Handle(
            GetPendingLeaveRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var result =
                await _leaveRequestRepository.GetPagedAsync(
                    request.OrganizationId,
                    request.EmployeeId,
                    request.DepartmentId,
                    LeaveRequestStatus.Pending.ToString(),
                    request.Search,
                    request.StartDate,
                    request.EndDate,
                    request.PageNumber,
                    request.PageSize,
                    request.SortBy,
                    request.SortDescending,
                    cancellationToken);

            return new PagedResponse<LeaveRequestListDto>
            {
                Items = result.Items
                    .Select(x => new LeaveRequestListDto
                    {
                        Id = x.Id,

                        EmployeeId = x.EmployeeId,

                        EmployeeName =
                            $"{x.Employee.FirstName} " +
                            $"{x.Employee.LastName}",

                        EmployeeNumber =
                            x.Employee.EmployeeNumber,

                        DepartmentId =
                            x.Employee.DepartmentId,

                        DepartmentName =
                            x.Employee.Department?.Name,

                        LeaveTypeId =
                            x.LeaveTypeId,

                        LeaveTypeName =
                            x.LeaveType.Name,

                        StartDate =
                            x.StartDate,

                        EndDate =
                            x.EndDate,

                        TotalDays =
                            (int)x.TotalDays,

                        Reason =
                            x.Reason,

                        Status =
                            x.Status.ToString(),

                        ApprovedBy =
                            x.ApprovedBy,

                        ApprovedOn =
                            x.ApprovedOn,

                        RejectionReason =
                            x.RejectionReason
                    })
                    .ToList(),

                PageNumber =
                    result.PageNumber,

                PageSize =
                    result.PageSize,

                //TotalCount =
                //    result.TotalCount
            };
        }
    }
}
