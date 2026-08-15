using HRMS.Application.Features.Employees.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
