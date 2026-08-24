using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.MarkPayrollAsPaid
{
    public class MarkPayrollAsPaidCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
