using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Enums
{
    public enum PermissionType
    {
        CreateEmployee = 1,
        UpdateEmployee = 2,
        DeleteEmployee = 3,
        ViewEmployee = 4,
        ManageRoles = 5,
        ManagePayroll = 6,
        ManageLeave = 7
    }
}
