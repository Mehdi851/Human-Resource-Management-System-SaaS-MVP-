using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.Payroll.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Queries.GetPayrolls
{
    public class GetPayrollsQueryHandler
         : IRequestHandler<
             GetPayrollsQuery,
             IReadOnlyList<PayrollDto>>
    {
        private readonly IPayrollRepository _repository;

        public GetPayrollsQueryHandler(
            IPayrollRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<PayrollDto>> Handle(
            GetPayrollsQuery request,
            CancellationToken cancellationToken)
        {
            var payrolls = await _repository.GetListAsync(
                request.OrganizationId,
                cancellationToken);

            return payrolls
                .Select(MapToDto)
                .ToList();
        }

        private static PayrollDto MapToDto(
            Domain.Entities.Payroll payroll)
        {
            return new PayrollDto
            {
                Id = payroll.Id,
                OrganizationId = payroll.OrganizationId,
                PayrollPeriodStart =
                    payroll.PayrollPeriodStart,
                PayrollPeriodEnd =
                    payroll.PayrollPeriodEnd,
                Status =
                    payroll.Status.ToString(),

                TotalBasicSalary =
                    payroll.PayrollItems.Sum(x => x.BasicSalary),

                TotalAllowances =
                    payroll.PayrollItems.Sum(x => x.Allowances),

                TotalDeductions =
                    payroll.PayrollItems.Sum(x => x.Deductions),

                TotalGrossSalary =
                    payroll.PayrollItems.Sum(x => x.GrossSalary),

                TotalNetSalary =
                    payroll.PayrollItems.Sum(x => x.NetSalary),

                Items = payroll.PayrollItems
                    .Select(x => new PayrollItemDto
                    {
                        Id = x.Id,
                        EmployeeId = x.EmployeeId,
                        BasicSalary = x.BasicSalary,
                        Allowances = x.Allowances,
                        Deductions = x.Deductions,
                        GrossSalary = x.GrossSalary,
                        NetSalary = x.NetSalary
                    })
                    .ToList()
            };
        }
    }
}
