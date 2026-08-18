using HRMS.Application.Features.LeaveRequests.Commands.ApproveLeaveRequest;
using HRMS.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using HRMS.Application.Features.LeaveRequests.Commands.RejectLeaveRequest;
using HRMS.Application.Features.LeaveRequests.Commands.SubmitLeaveRequest;
using HRMS.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using HRMS.Application.Features.LeaveRequests.Queries.GetEmployeeLeaveRequests;
using HRMS.Application.Features.LeaveRequests.Queries.GetLeaveRequestById;
using HRMS.Application.Features.LeaveRequests.Queries.GetLeaveRequests;
using HRMS.Application.Features.LeaveRequests.Queries.GetPendingLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/LeaveRequests
        [HttpPost]
        public async Task<IActionResult> Submit(
            [FromBody] SubmitLeaveRequestCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        // GET: api/LeaveRequests
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetLeaveRequestsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        // GET: api/LeaveRequests/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var query = new GetLeaveRequestByIdQuery
            {
                Id = id,
                OrganizationId = organizationId
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/LeaveRequests/employee/{employeeId}
        [HttpGet("employee/{employeeId:guid}")]
        public async Task<IActionResult> GetEmployeeLeaveRequests(
            Guid employeeId,
            [FromQuery] Guid organizationId,
            [FromQuery] string? status,
            [FromQuery] string? search,
            [FromQuery] DateOnly? startDate,
            [FromQuery] DateOnly? endDate,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDescending = false,
            CancellationToken cancellationToken = default)
        {
            var query = new GetEmployeeLeaveRequestsQuery
            {
                OrganizationId = organizationId,
                EmployeeId = employeeId,
                Status = status,
                Search = search,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDescending = sortDescending
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        // GET: api/LeaveRequests/pending
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending(
            [FromQuery] GetPendingLeaveRequestsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        // PUT: api/LeaveRequests/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateLeaveRequestCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            await _mediator.Send(
                command,
                cancellationToken);

            return NoContent();
        }

        // DELETE: api/LeaveRequests/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Cancel(
            Guid id,
            [FromQuery] Guid organizationId,
            CancellationToken cancellationToken)
        {
            var command = new CancelLeaveRequestCommand
            {
                Id = id,
                OrganizationId = organizationId
            };

            await _mediator.Send(
                command,
                cancellationToken);

            return NoContent();
        }

        // POST: api/LeaveRequests/{id}/approve
        [HttpPost("{id:guid}/approve")]
        public async Task<IActionResult> Approve(
            Guid id,
            [FromBody] ApproveLeaveRequestCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            await _mediator.Send(
                command,
                cancellationToken);

            return NoContent();
        }

        // POST: api/LeaveRequests/{id}/reject
        [HttpPost("{id:guid}/reject")]
        public async Task<IActionResult> Reject(
            Guid id,
            [FromBody] RejectLeaveRequestCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            await _mediator.Send(
                command,
                cancellationToken);

            return NoContent();
        }
    }
}
