using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<CreateDepartmentResponse>
    {
        public Guid OrganizationId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public Guid? ManagerId { get; set; }
    }
}
