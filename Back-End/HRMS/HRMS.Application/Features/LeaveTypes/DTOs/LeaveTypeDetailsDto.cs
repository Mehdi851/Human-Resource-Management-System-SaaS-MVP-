using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.LeaveTypes.DTOs
{
    public class LeaveTypeDetailsDto
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public bool IsPaid { get; set; }

        public int DefaultDays { get; set; }
    }
}
