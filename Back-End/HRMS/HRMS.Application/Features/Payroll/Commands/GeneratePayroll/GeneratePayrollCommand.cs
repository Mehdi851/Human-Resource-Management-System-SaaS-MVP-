using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.GeneratePayroll
{
    public class GeneratePayrollCommand : IRequest<Guid>
    {
        public Guid OrganizationId { get; set; }

        public DateOnly PayrollPeriodStart { get; set; }

        public DateOnly PayrollPeriodEnd { get; set; }
    }
}
