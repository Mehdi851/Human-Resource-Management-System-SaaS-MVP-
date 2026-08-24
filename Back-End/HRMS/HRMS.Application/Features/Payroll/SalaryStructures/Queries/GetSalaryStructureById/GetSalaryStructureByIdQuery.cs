using HRMS.Application.Features.Payroll.SalaryStructures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.Queries.GetSalaryStructureById
{
    public class GetSalaryStructureByIdQuery
        : IRequest<SalaryStructureDto>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
