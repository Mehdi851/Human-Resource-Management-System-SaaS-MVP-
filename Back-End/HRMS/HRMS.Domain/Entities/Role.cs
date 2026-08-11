using HRMS.Domain.Common;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        public Guid OrganizationId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public RoleType Type { get; set; }

        // Relationships

        [Required]
        public virtual Organization Organization { get; set; } = null!;

        public virtual ICollection<AppUser> Users { get; set; }
            = new HashSet<AppUser>();

        public virtual ICollection<Permission> Permissions { get; set; }
            = new HashSet<Permission>();
    }
}
