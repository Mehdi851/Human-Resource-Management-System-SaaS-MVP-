using HRMS.Application.Features.Payroll.SalaryStructures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.Queries.GetSalaryStructures
{
    public class GetSalaryStructuresQuery
        : IRequest<IReadOnlyList<SalaryStructureListDto>>
    {
        public Guid OrganizationId { get; set; }

        public Guid? EmployeeId { get; set; }
    }
}
