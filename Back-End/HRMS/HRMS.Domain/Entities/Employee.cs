using HRMS.Domain.Common;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public Guid OrganizationId { get; set; }

        public Guid? UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string EmployeeNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(30)]
        public string? PhoneNumber { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? DesignationId { get; set; }

        public Guid? ManagerId { get; set; }

        [Required]
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

        // Relationships

        [Required]
        public virtual Organization Organization { get; set; } = null!;

        public virtual AppUser? User { get; set; }

        public virtual Department? Department { get; set; }

        public virtual Designation? Designation { get; set; }

        public virtual Employee? Manager { get; set; }

        public virtual ICollection<Employee> Subordinates { get; set; }
            = new HashSet<Employee>();

        // Employee can have multiple leave requests
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
            = new List<LeaveRequest>();
    }
}
