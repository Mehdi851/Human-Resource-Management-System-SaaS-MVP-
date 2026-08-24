using HRMS.Application.Features.Payroll.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Queries.GetPayrolls
{
    public class GetPayrollsQuery
       : IRequest<IReadOnlyList<PayrollDto>>
    {
        public Guid OrganizationId { get; set; }
    }
}
