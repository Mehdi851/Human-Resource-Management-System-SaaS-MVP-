using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.Features.LeaveRequests.Queries.GetLeaveRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Queries.GetEmployeeLeaveRequests
{
    public class GetEmployeeLeaveRequestsHandler
    : IRequestHandler<
        GetEmployeeLeaveRequestsQuery,
        PagedResponse<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository
            _leaveRequestRepository;

        public GetEmployeeLeaveRequestsHandler(
            ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository =
                leaveRequestRepository;
        }

        public async Task<PagedResponse<LeaveRequestListDto>> Handle(
            GetEmployeeLeaveRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var result =
                await _leaveRequestRepository.GetPagedAsync(
                    request.OrganizationId,
                    request.EmployeeId,
                    null,
                    request.Status,
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
