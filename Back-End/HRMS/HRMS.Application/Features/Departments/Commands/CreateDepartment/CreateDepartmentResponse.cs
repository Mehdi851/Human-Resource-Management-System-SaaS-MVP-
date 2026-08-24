using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentResponse
    {
        public Guid Id { get; set; }
        public string Message { get; internal set; }
    }
}
