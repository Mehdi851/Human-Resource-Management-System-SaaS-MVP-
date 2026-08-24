using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Authentication.Authorization
{
    public static class AuthorizationPolicies
    {
        public const string SuperAdminOnly = "SuperAdminOnly";

        public const string HRAdminOnly = "HRAdminOnly";

        public const string HRAdminOrSuperAdmin =
            "HRAdminOrSuperAdmin";

        public const string EmployeeAccess =
            "EmployeeAccess";
    }
}
