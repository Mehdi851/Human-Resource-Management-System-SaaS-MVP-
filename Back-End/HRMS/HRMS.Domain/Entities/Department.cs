using HRMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class Department : BaseEntity
    {
        [Required]
        public Guid OrganizationId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        // Relationships

        [Required]
        public virtual Organization Organization { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
            = new HashSet<Employee>();

        public virtual ICollection<Designation> Designations { get; set; }
            = new HashSet<Designation>();
    }
}
