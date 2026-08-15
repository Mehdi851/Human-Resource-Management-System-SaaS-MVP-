using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<UpdateEmployeeResponse>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? EmployeeNumber { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
