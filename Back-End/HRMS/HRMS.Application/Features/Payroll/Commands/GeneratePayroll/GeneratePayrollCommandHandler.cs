using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using PayrollEntity = HRMS.Domain.Entities.Payroll;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.GeneratePayroll
{
    public class GeneratePayrollCommandHandler
        : IRequestHandler<GeneratePayrollCommand, Guid>
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly ISalaryStructureRepository _salaryStructureRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GeneratePayrollCommandHandler(
            IPayrollRepository payrollRepository,
            ISalaryStructureRepository salaryStructureRepository,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _payrollRepository = payrollRepository;
            _salaryStructureRepository = salaryStructureRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(
            GeneratePayrollCommand request,
            CancellationToken cancellationToken)
        {
            // -------------------------------------------------
            // 1. Prevent duplicate payroll
            // -------------------------------------------------

            var payrollExists =
                await _payrollRepository.ExistsForPeriodAsync(
                    request.OrganizationId,
                    request.PayrollPeriodStart,
                    request.PayrollPeriodEnd,
                    cancellationToken);

            if (payrollExists)
            {
                throw new InvalidOperationException(
                    "Payroll already exists for the specified period.");
            }

            // -------------------------------------------------
            // 2. Get active employees
            // -------------------------------------------------

            var employees =
                await _employeeRepository.GetActiveEmployeesByOrganizationIdAsync(
                    request.OrganizationId,
                    cancellationToken);

            var activeEmployees = employees
                .Where(x => !x.IsDeleted)
                .ToList();

            if (!activeEmployees.Any())
            {
                throw new InvalidOperationException(
                    "No active employees were found for the organization.");
            }

            // -------------------------------------------------
            // 3. Get effective salary structures
            // -------------------------------------------------

            var salaryStructures =
                await _salaryStructureRepository
                    .GetEffectiveForEmployeesAsync(
                        request.OrganizationId,
                        request.PayrollPeriodStart,
                        request.PayrollPeriodEnd,
                        cancellationToken);

            // -------------------------------------------------
            // 4. Create payroll
            // -------------------------------------------------

            var payroll = new PayrollEntity
            {
                Id = Guid.NewGuid(),
                OrganizationId = request.OrganizationId,
                PayrollPeriodStart = request.PayrollPeriodStart,
                PayrollPeriodEnd = request.PayrollPeriodEnd,
                Status = PayrollStatus.Processed
            };

            // -------------------------------------------------
            // 5. Generate payroll items
            // -------------------------------------------------

            foreach (var employee in activeEmployees)
            {
                var salaryStructure = salaryStructures
                    .Where(x => x.EmployeeId == employee.Id)
                    .OrderByDescending(x => x.EffectiveFrom)
                    .FirstOrDefault();

                if (salaryStructure == null)
                {
                    throw new InvalidOperationException(
                        $"No active salary structure was found for employee {employee.Id}.");
                }

                var grossSalary =
                    salaryStructure.BasicSalary +
                    salaryStructure.Allowances;

                var netSalary =
                    grossSalary -
                    salaryStructure.Deductions;

                var payrollItem = new PayrollItem
                {
                    Id = Guid.NewGuid(),
                    OrganizationId = request.OrganizationId,
                    PayrollId = payroll.Id,
                    EmployeeId = employee.Id,

                    BasicSalary =
                        salaryStructure.BasicSalary,

                    Allowances =
                        salaryStructure.Allowances,

                    Deductions =
                        salaryStructure.Deductions,

                    GrossSalary =
                        grossSalary,

                    NetSalary =
                        netSalary
                };

                payroll.PayrollItems.Add(payrollItem);
            }

            // -------------------------------------------------
            // 6. Persist payroll
            // -------------------------------------------------

            await _payrollRepository.AddAsync(
                payroll,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return payroll.Id;
        }
    }
}
