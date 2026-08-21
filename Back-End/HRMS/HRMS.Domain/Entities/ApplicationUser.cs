using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Entities
{
    public class ApplicationUser 
    {
        public Guid OrganizationId { get; set; }

        public Organization Organization { get; set; } = default!;
    }
}
