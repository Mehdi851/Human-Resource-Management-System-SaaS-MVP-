using HRMS.Application.Features.Payroll.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Queries.GetPayrollById
{
    public class GetPayrollByIdQuery : IRequest<PayrollDto>
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
