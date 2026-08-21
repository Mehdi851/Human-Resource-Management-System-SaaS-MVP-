using HRMS.Domain.Common;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public Guid OrganizationId { get; set; }

        public Guid EmployeeId { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public TimeOnly? CheckInTime { get; set; }

        public TimeOnly? CheckOutTime { get; set; }

        public AttendanceStatus Status { get; set; }

        public TimeSpan? WorkingHours { get; private set; }

        public string? Remarks { get; set; }

        // Navigation Properties

        public Organization Organization { get; set; } = default!;

        public Employee Employee { get; set; } = default!;

        /// <summary>
        /// Calculates working hours from check-in and check-out.
        /// WorkingHours is intentionally calculated server-side and is
        /// not directly writable by API clients.
        /// </summary>
        public void CalculateWorkingHours()
        {
            if (!CheckInTime.HasValue || !CheckOutTime.HasValue)
            {
                WorkingHours = null;
                return;
            }

            if (CheckOutTime.Value < CheckInTime.Value)
            {
                throw new InvalidOperationException(
                    "Check-out time cannot be earlier than check-in time.");
            }

            WorkingHours = CheckOutTime.Value - CheckInTime.Value;
        }
    }
}
