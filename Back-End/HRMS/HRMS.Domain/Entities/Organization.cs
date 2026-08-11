using HRMS.Domain.Common;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class Organization : BaseEntity
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Slug { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(255)]
        public string? ContactEmail { get; set; }

        [Phone]
        [StringLength(30)]
        public string? ContactPhone { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [Required]
        public OrganizationStatus Status { get; set; } = OrganizationStatus.Active;

        public DateTime? SubscriptionStartDate { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        // Navigation Properties

        public virtual ICollection<AppUser> Users { get; set; }
            = new HashSet<AppUser>();

        public virtual ICollection<Employee> Employees { get; set; }
            = new HashSet<Employee>();

        public virtual ICollection<Department> Departments { get; set; }
            = new HashSet<Department>();

        public virtual ICollection<Designation> Designations { get; set; }
            = new HashSet<Designation>();
    }
}
