using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public Guid? ManagerId { get; set; }
    }
}
