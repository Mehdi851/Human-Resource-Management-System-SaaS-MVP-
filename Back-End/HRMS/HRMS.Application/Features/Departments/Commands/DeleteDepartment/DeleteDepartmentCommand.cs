using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
