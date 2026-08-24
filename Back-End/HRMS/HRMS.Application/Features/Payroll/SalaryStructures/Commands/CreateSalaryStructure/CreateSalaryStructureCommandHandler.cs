using HRMS.Application.Common.Interfaces;
using HRMS.Application.Features.Payroll.SalaryStructures.DTOs;
using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.Commands.CreateSalaryStructure
{
    public class CreateSalaryStructureCommandHandler
        : IRequestHandler<CreateSalaryStructureCommand, SalaryStructureDto>
    {
        private readonly ISalaryStructureRepository _salaryStructureRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSalaryStructureCommandHandler(
            ISalaryStructureRepository salaryStructureRepository,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _salaryStructureRepository = salaryStructureRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SalaryStructureDto> Handle(
            CreateSalaryStructureCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(
                request.EmployeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }

            if (employee.OrganizationId != request.OrganizationId)
            {
                throw new InvalidOperationException(
                    "Employee does not belong to the specified organization.");
            }

            // Adjust this property according to the existing Employee entity.
            if (employee.IsDeleted)
            {
                throw new InvalidOperationException(
                    "Salary structure cannot be created for a deleted employee.");
            }

            var hasOverlap =
                await _salaryStructureRepository.HasOverlappingStructureAsync(
                    request.EmployeeId,
                    request.EffectiveFrom,
                    request.EffectiveTo,
                    null,
                    cancellationToken);

            if (hasOverlap)
            {
                throw new InvalidOperationException(
                    "The employee already has an active salary structure covering the specified effective period.");
            }

            var salaryStructure = new SalaryStructure
            {
                Id = Guid.NewGuid(),
                OrganizationId = request.OrganizationId,
                EmployeeId = request.EmployeeId,
                BasicSalary = request.BasicSalary,
                Allowances = request.Allowances,
                Deductions = request.Deductions,
                EffectiveFrom = request.EffectiveFrom,
                EffectiveTo = request.EffectiveTo,
                PaymentFrequency = request.PaymentFrequency,
                Status = SalaryStructureStatus.Active
            };

            await _salaryStructureRepository.AddAsync(
                salaryStructure,
                cancellationToken);

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
