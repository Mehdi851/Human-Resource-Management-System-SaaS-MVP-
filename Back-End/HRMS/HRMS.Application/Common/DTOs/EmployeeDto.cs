using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DepartmentName { get; set; }
    }
}
