using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.ApprovePayroll
{
    public class ApprovePayrollCommandHandler
       : IRequestHandler<ApprovePayrollCommand, Guid>
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApprovePayrollCommandHandler(
            IPayrollRepository payrollRepository,
            IUnitOfWork unitOfWork)
        {
            _payrollRepository = payrollRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(
            ApprovePayrollCommand request,
            CancellationToken cancellationToken)
        {
            var payroll =
                await _payrollRepository.GetForUpdateAsync(
                    request.Id,
                    request.OrganizationId,
                    cancellationToken);

            if (payroll == null)
            {
                throw new KeyNotFoundException(
                    "Payroll not found.");
            }

            if (payroll.Status != PayrollStatus.Processed)
            {
                throw new InvalidOperationException(
                    $"Payroll cannot be approved because its current status is {payroll.Status}.");
            }

            payroll.Status = PayrollStatus.Approved;

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return payroll.Id;
        }
    }
}
