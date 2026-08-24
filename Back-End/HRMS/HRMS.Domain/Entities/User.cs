using HRMS.Domain.Common;
using HRMS.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        [Required]
        public Guid OrganizationId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        public DateTime? LastLoginAt { get; set; }

        public UserStatus Status { get; set; } = UserStatus.Active;

        public Organization Organization { get; set; } = null!;

        public Employee? Employee { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
