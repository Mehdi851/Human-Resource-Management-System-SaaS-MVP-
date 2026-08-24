using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.Payroll.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Queries.GetPayrollSummary
{
    public class GetPayrollSummaryQueryHandler
        : IRequestHandler<
            GetPayrollSummaryQuery,
            PayrollSummaryDto>
    {
        private readonly IPayrollRepository _payrollRepository;

        public GetPayrollSummaryQueryHandler(
            IPayrollRepository payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }

        public async Task<PayrollSummaryDto> Handle(
            GetPayrollSummaryQuery request,
            CancellationToken cancellationToken)
        {
            var payroll =
                await _payrollRepository.GetByPeriodAsync(
                    request.OrganizationId,
                    request.PayrollPeriodStart,
                    request.PayrollPeriodEnd,
                    cancellationToken);

            if (payroll == null)
            {
                throw new KeyNotFoundException(
                    "Payroll not found for the specified period.");
            }

            return new PayrollSummaryDto
            {
                PayrollId = payroll.Id,

                OrganizationId =
                    payroll.OrganizationId,

                PayrollPeriodStart =
                    payroll.PayrollPeriodStart,

                PayrollPeriodEnd =
                    payroll.PayrollPeriodEnd,

                Status =
                    payroll.Status.ToString(),

                TotalEmployees =
                    payroll.PayrollItems.Count,

                TotalBasicSalary =
                    payroll.PayrollItems
                        .Sum(x => x.BasicSalary),

                TotalAllowances =
                    payroll.PayrollItems
                        .Sum(x => x.Allowances),

                TotalDeductions =
                    payroll.PayrollItems
                        .Sum(x => x.Deductions),

                TotalGrossSalary =
                    payroll.PayrollItems
                        .Sum(x => x.GrossSalary),

                TotalNetSalary =
                    payroll.PayrollItems
                        .Sum(x => x.NetSalary)
            };
        }
    }
}
