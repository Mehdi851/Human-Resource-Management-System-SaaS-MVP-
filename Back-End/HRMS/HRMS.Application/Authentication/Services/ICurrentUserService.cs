using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Services
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }

        Guid? OrganizationId { get; }

        string? Email { get; }

        string? Role { get; }

        bool IsAuthenticated { get; }
    }
}
