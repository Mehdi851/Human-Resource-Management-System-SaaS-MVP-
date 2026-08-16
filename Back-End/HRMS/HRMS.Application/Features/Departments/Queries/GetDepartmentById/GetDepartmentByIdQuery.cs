using HRMS.Application.Features.Departments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQuery
       : IRequest<DepartmentDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
