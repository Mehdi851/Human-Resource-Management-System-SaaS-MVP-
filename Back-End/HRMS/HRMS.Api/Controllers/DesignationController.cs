using HRMS.Application.Features.Designations.Commands.CreateDesignation;
using HRMS.Application.Features.Designations.Commands.DeleteDesignation;
using HRMS.Application.Features.Designations.Commands.UpdateDesignation;
using HRMS.Application.Features.Designations.Queries.GetDesignationById;
using HRMS.Application.Features.Designations.Queries.GetDesignations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DesignationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Designation
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDesignationCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        // GET: api/Designation/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetDesignationByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        // GET: api/Designation
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetDesignationsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                query,
                cancellationToken);

            return Ok(result);
        }

        // PUT: api/Designation/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateDesignationCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        // DELETE: api/Designation/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteDesignationCommand
            {
                Id = id
            };

            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }
    }
}
