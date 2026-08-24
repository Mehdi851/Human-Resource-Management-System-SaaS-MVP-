using HRMS.Application.Features.Payroll.SalaryStructures.DTOs;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.SalaryStructures.Commands.CreateSalaryStructure
{
    public class CreateSalaryStructureCommand : IRequest<SalaryStructureDto>
    {
        public Guid OrganizationId { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public DateOnly EffectiveFrom { get; set; }

        public DateOnly? EffectiveTo { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }
    }
}
