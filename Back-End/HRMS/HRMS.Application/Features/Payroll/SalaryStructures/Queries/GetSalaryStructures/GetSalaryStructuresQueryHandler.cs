using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.Payroll.SalaryStructures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.Queries.GetSalaryStructures
{
    public class GetSalaryStructuresQueryHandler
        : IRequestHandler<
            GetSalaryStructuresQuery,
            IReadOnlyList<SalaryStructureListDto>>
    {
        private readonly ISalaryStructureRepository _repository;

        public GetSalaryStructuresQueryHandler(
            ISalaryStructureRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<SalaryStructureListDto>> Handle(
            GetSalaryStructuresQuery request,
            CancellationToken cancellationToken)
        {
            var salaryStructures =
                await _repository.GetListAsync(
                    request.OrganizationId,
                    request.EmployeeId,
                    cancellationToken);

            return salaryStructures
                .Select(x => new SalaryStructureListDto
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    BasicSalary = x.BasicSalary,
                    Allowances = x.Allowances,
                    Deductions = x.Deductions,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    PaymentFrequency =
                        x.PaymentFrequency.ToString(),
                    Status =
                        x.Status.ToString()
                })
                .ToList();
        }
    }
}
