using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.Payroll.SalaryStructures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.Queries.GetSalaryStructureById
{
    public class GetSalaryStructureByIdQueryHandler
        : IRequestHandler<GetSalaryStructureByIdQuery, SalaryStructureDto>
    {
        private readonly ISalaryStructureRepository _repository;

        public GetSalaryStructureByIdQueryHandler(
            ISalaryStructureRepository repository)
        {
            _repository = repository;
        }

        public async Task<SalaryStructureDto> Handle(
            GetSalaryStructureByIdQuery request,
            CancellationToken cancellationToken)
        {
            var salaryStructure = await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (salaryStructure == null ||
                salaryStructure.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Salary structure not found.");
            }

            return new SalaryStructureDto
            {
                Id = salaryStructure.Id,
                OrganizationId = salaryStructure.OrganizationId,
                EmployeeId = salaryStructure.EmployeeId,
                BasicSalary = salaryStructure.BasicSalary,
                Allowances = salaryStructure.Allowances,
                Deductions = salaryStructure.Deductions,
                EffectiveFrom = salaryStructure.EffectiveFrom,
                EffectiveTo = salaryStructure.EffectiveTo,
                PaymentFrequency =
                    salaryStructure.PaymentFrequency.ToString(),
                Status =
                    salaryStructure.Status.ToString()
            };
        }
    }
}
