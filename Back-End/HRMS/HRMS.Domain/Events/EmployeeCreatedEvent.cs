using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Domain.Events
{
    public class EmployeeCreatedEvent
    {
        public Guid EmployeeId { get; set; }
        public string Email { get; set; }
    }
}
