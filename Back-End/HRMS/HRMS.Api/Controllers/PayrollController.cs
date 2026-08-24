using HRMS.Application.Features.Payroll.Commands.ApprovePayroll;
using HRMS.Application.Features.Payroll.Commands.CancelPayroll;
using HRMS.Application.Features.Payroll.Commands.GeneratePayroll;
using HRMS.Application.Features.Payroll.Commands.MarkPayrollAsPaid;
using HRMS.Application.Features.Payroll.Queries.GetPayrollById;
using HRMS.Application.Features.Payroll.Queries.GetPayrolls;
using HRMS.Application.Features.Payroll.Queries.GetPayrollSummary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Authorize(Roles = "SuperAdmin,HRAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PayrollController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Payroll
        [HttpPost]
        public async Task<IActionResult> Generate(
            [FromBody] GeneratePayrollCommand command,
            CancellationToken cancellationToken)
        {
            var payrollId = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(new
            {
                id = payrollId,
                message = "Payroll generated successfully."
            });
        }

        // GET: api/Payroll/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var query = new GetPayrollByIdQuery
            {
                Id = id,
                OrganizationId = organizationId
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        // GET: api/Payroll
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var query = new GetPayrollsQuery
            {
                OrganizationId = organizationId
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        // POST: api/Payroll/{id}/approve
        [HttpPost("{id:guid}/approve")]
        public async Task<IActionResult> Approve(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var command = new ApprovePayrollCommand
            {
                Id = id,
                OrganizationId = organizationId
            };

            var payrollId = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(new
            {
                id = payrollId,
                message = "Payroll approved successfully."
            });
        }

        // POST: api/Payroll/{id}/pay
        [HttpPost("{id:guid}/pay")]
        public async Task<IActionResult> MarkAsPaid(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var command = new MarkPayrollAsPaidCommand
            {
                Id = id,
                OrganizationId = organizationId
            };

            var payrollId = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(new
            {
                id = payrollId,
                message = "Payroll marked as paid successfully."
            });
        }

        // POST: api/Payroll/{id}/cancel
        [HttpPost("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var command = new CancelPayrollCommand
            {
                Id = id,
                OrganizationId = organizationId
            };

            var payrollId = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(new
            {
                id = payrollId,
                message = "Payroll cancelled successfully."
            });
        }

        // GET: api/Payroll/summary
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary(
            [FromQuery] Guid organizationId,
            [FromQuery] DateOnly payrollPeriodStart,
            [FromQuery] DateOnly payrollPeriodEnd,
            CancellationToken cancellationToken)
        {
            var query = new GetPayrollSummaryQuery
            {
                OrganizationId = organizationId,
                PayrollPeriodStart = payrollPeriodStart,
                PayrollPeriodEnd = payrollPeriodEnd
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }
    }
}
