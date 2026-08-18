using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveRequests.Queries.GetLeaveRequestById
{
    public class LeaveRequestDetailsDto
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid EmployeeId { get; set; }

        public string EmployeeName { get; set; } = default!;

        public string? EmployeeNumber { get; set; }

        public Guid? DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public Guid LeaveTypeId { get; set; }

        public string LeaveTypeName { get; set; } = default!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int TotalDays { get; set; }

        public string? Reason { get; set; }

        public string Status { get; set; } = default!;

        public Guid? ApprovedBy { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public string? RejectionReason { get; set; }
    }
}
