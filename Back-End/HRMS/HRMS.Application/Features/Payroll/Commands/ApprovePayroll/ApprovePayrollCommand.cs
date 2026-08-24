using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.ApprovePayroll
{
    public class ApprovePayrollCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
