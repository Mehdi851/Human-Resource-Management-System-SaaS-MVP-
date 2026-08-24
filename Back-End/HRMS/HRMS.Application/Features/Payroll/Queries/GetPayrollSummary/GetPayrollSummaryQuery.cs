using HRMS.Application.Features.Payroll.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Queries.GetPayrollSummary
{
    public class GetPayrollSummaryQuery
        : IRequest<PayrollSummaryDto>
    {
        public Guid OrganizationId { get; set; }

        public DateOnly PayrollPeriodStart { get; set; }

        public DateOnly PayrollPeriodEnd { get; set; }
    }
}
