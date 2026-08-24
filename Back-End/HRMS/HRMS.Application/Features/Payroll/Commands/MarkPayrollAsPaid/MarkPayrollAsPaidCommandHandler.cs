using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Features.Payroll.Commands.MarkPayrollAsPaid
{
    public class MarkPayrollAsPaidCommandHandler
        : IRequestHandler<MarkPayrollAsPaidCommand, Guid>
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkPayrollAsPaidCommandHandler(
            IPayrollRepository payrollRepository,
            IUnitOfWork unitOfWork)
        {
            _payrollRepository = payrollRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(
            MarkPayrollAsPaidCommand request,
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

            if (payroll.Status != PayrollStatus.Approved)
            {
                throw new InvalidOperationException(
                    $"Payroll cannot be marked as paid because its current status is {payroll.Status}.");
            }

            payroll.Status = PayrollStatus.Paid;

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return payroll.Id;
        }
    }
}
