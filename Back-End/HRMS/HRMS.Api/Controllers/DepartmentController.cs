using HRMS.Application.Common.Models;
using HRMS.Application.Features.Departments.Commands.CreateDepartment;
using HRMS.Application.Features.Departments.Commands.DeleteDepartment;
using HRMS.Application.Features.Departments.Commands.UpdateDepartment;
using HRMS.Application.Features.Departments.DTOs;
using HRMS.Application.Features.Departments.Queries.GetDepartmentById;
using HRMS.Application.Features.Departments.Queries.GetDepartments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateDepartmentResponse>> Create(
            CreateDepartmentCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DepartmentListItemDto>>> GetAll(
            [FromQuery] GetDepartmentsQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DepartmentDetailsDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(
                new GetDepartmentByIdQuery
                {
                    Id = id
                });

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateDepartmentCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route id and request id do not match.");

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(
                new DeleteDepartmentCommand
                {
                    Id = id
                });

            return NoContent();
        }
    }
}
