using HRMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class Permission : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Resource { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Action { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        // Relationships

        public virtual ICollection<Role> Roles { get; set; }
            = new HashSet<Role>();
    }
}
