    using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.DTOs
{
    public class RegisterUserResponse
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = default!;

        public string Role { get; set; } = default!;

        public Guid OrganizationId { get; set; }
    }
}
