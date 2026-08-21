using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Attandance.DTOs
{
    public class AttendanceListDto
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public string EmployeeName { get; set; } = default!;

        public string? EmployeeNumber { get; set; }

        public string? DepartmentName { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public TimeOnly? CheckInTime { get; set; }

        public TimeOnly? CheckOutTime { get; set; }

        public AttendanceStatus Status { get; set; }

        public TimeSpan? WorkingHours { get; set; }

        public string? Remarks { get; set; }
    }
}
