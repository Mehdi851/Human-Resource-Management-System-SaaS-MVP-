using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.CancelPayroll
{
    public class CancelPayrollCommandHandler
        : IRequestHandler<CancelPayrollCommand, Guid>
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelPayrollCommandHandler(
            IPayrollRepository payrollRepository,
            IUnitOfWork unitOfWork)
        {
            _payrollRepository = payrollRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(
            CancelPayrollCommand request,
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

            if (payroll.Status == PayrollStatus.Paid)
            {
                throw new InvalidOperationException(
                    "Paid payroll cannot be cancelled.");
            }

            if (payroll.Status == PayrollStatus.Cancelled)
            {
                throw new InvalidOperationException(
                    "Payroll is already cancelled.");
            }

            if (payroll.Status != PayrollStatus.Processed &&
                payroll.Status != PayrollStatus.Approved)
            {
                throw new InvalidOperationException(
                    $"Payroll cannot be cancelled because its current status is {payroll.Status}.");
            }

            payroll.Status = PayrollStatus.Cancelled;

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return payroll.Id;
        }
    }
}
