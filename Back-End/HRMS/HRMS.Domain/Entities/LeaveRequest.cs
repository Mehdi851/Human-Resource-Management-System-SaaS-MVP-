using HRMS.Domain.Common;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class LeaveRequest : BaseEntity
    {
        public Guid OrganizationId { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid LeaveTypeId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public decimal TotalDays { get; set; }

        public string? Reason { get; set; }

        public LeaveRequestStatus Status { get; set; }

        public Guid? ApprovedBy { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public string? RejectionReason { get; set; }

        public virtual Employee Employee { get; set; } = default!;

        public virtual LeaveType LeaveType { get; set; } = default!;
    }
}
