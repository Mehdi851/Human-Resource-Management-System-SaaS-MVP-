using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.Payroll.SalaryStructures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.Commands.UpdateSalaryStructure
{
    public class UpdateSalaryStructureCommandHandler
        : IRequestHandler<
            UpdateSalaryStructureCommand,
            SalaryStructureDto>
    {
        private readonly ISalaryStructureRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSalaryStructureCommandHandler(
            ISalaryStructureRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SalaryStructureDto> Handle(
            UpdateSalaryStructureCommand request,
            CancellationToken cancellationToken)
        {
            var salaryStructure =
                await _repository.GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (salaryStructure == null ||
                salaryStructure.OrganizationId != request.OrganizationId)
            {
                throw new KeyNotFoundException(
                    "Salary structure not found.");
            }

            var hasOverlap =
                await _repository.HasOverlappingStructureAsync(
                    salaryStructure.EmployeeId,
                    request.EffectiveFrom,
                    request.EffectiveTo,
                    request.Id,
                    cancellationToken);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "The employee already has another active salary structure covering the specified effective period.");
            }

            salaryStructure.BasicSalary = request.BasicSalary;
            salaryStructure.Allowances = request.Allowances;
            salaryStructure.Deductions = request.Deductions;
            salaryStructure.EffectiveFrom = request.EffectiveFrom;
            salaryStructure.EffectiveTo = request.EffectiveTo;
            salaryStructure.PaymentFrequency = request.PaymentFrequency;
            salaryStructure.Status = request.Status;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
