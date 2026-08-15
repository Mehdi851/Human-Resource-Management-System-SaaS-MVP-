using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand  : IRequest<DeleteEmployeeResponse>
    {
        public Guid Id { get; set; }
    }
}
