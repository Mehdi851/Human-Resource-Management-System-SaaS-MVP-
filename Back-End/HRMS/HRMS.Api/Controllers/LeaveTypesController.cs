using HRMS.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using HRMS.Application.Features.LeaveTypes.Commands.DeleteLeaveType;
using HRMS.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using HRMS.Application.Features.LeaveTypes.Queries.GetLeaveTypeById;
using HRMS.Application.Features.LeaveTypes.Queries.GetLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateLeaveTypeCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    id = result.Id,
                    organizationId = result.OrganizationId
                },
                result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(
       [FromQuery] GetLeaveTypesQuery query,
       CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
        Guid id,
        [FromQuery] Guid organizationId,
        CancellationToken cancellationToken)
        {
            var query = new GetLeaveTypeByIdQuery
            {
                Id = id,
                OrganizationId = organizationId
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateLeaveTypeCommand command,
        CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
        Guid id,
        [FromQuery] Guid organizationId,
        CancellationToken cancellationToken)
        {
            var command = new DeleteLeaveTypeCommand
            {
                Id = id,
                OrganizationId = organizationId
            };

            await _mediator.Send(
                command,
                cancellationToken);

            return NoContent();
        }
    }
}
