using HRMS.Application.Features.Employees.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeResponse>
    {
        public CreateEmployeeDto Employee { get; set; } = new();
    }
}
