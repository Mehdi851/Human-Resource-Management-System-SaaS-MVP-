using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.CancelPayroll
{
    public class CancelPayrollCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }   
}
