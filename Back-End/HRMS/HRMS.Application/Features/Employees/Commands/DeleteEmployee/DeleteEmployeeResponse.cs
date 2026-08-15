using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
           = "Employee deleted successfully.";
    }
}
